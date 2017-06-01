//------------------------------------------------------------------------------
// <copyright file="CommandGenerateEntity.cs" company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using System.IO;
using EntityGenerator;

namespace GenerateExtension
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CommandGenerateEntity
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("43701e36-aa7f-4046-8bd0-9aeaad31a09a");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandGenerateEntity"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private CommandGenerateEntity(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CommandGenerateEntity Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new CommandGenerateEntity(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var dte = (DTE)ServiceProvider.GetService(typeof(SDTE));
            //IVsUIShell uiShell = (IVsUIShell)ServiceProvider.GetService(typeof(SVsUIShell));

            string slnPath = Path.GetDirectoryName(dte.Solution.FullName);

            var selectedItems = dte.SelectedItems;

            if (!selectedItems.MultiSelect && selectedItems.Count == 1)
            {
                string dir = string.Empty;
                var selItemObj = selectedItems.Item(1);
                if (selItemObj.Project is Project)
                {
                    dir = Path.GetDirectoryName(selItemObj.Project.FullName);
                }
                else
                {
                    dir = selItemObj.ProjectItem.FileNames[1];
                }

                SlnInfo sInfo = new SlnInfo()
                {
                    SelectedDir = dir,
                    SlnPath = slnPath
                };

                string filePath = GenerateFile(sInfo);

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    dte.ItemOperations.AddExistingItem(filePath);
                    dte.ItemOperations.OpenFile(filePath);
                }
            }

        }

        private string GenerateFile(SlnInfo sInfo)
        {
            try
            {
                Main form = new Main(sInfo);
                var digRet = form.ShowDialog();
                if (!form.IsDisposed && digRet == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(form.Content))
                    {
                        string filePath = Path.Combine(sInfo.SelectedDir, form.FileName);
                        File.WriteAllText(filePath, form.Content);
                        return filePath;
                    }
                    return "";
                }
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }

        }

        private void ShowMsg(string message, string title)
        {
            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.ServiceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
