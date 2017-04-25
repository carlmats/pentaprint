﻿using PentaPrint.View;
using PentaPrint.Model;
using PentaPrint.View.Input;
using PentaPrint.View.InputGroup;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PentaPrint.Devices;

namespace PentaPrint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Printer printer = new Printer();
        
        public MainWindow()
        {
            InitializeComponent();
            SetupFields();
            SetupMenu();
        }

        private void SetupMenu()
        {
            var menu = this.PrintMenu;
            foreach (string item in Properties.Settings.Default.InputFields)
            {
                var menuItem = new MenuItem();
                menuItem.Header = item;
                menu.Items.Add(menuItem);
            }
        }

        private void SetupFields()
        {
            var inputPanel = this.InputContent;
            MainEngineBarcode m = new MainEngineBarcode();
            m.GetPrint();
            var verifyPanel = this.VerifyContent;

            setupInputFields(inputPanel);

            //setupVerifyFields(verifyPanel);
        }

        private void setupVerifyFields(StackPanel verifyPanel)
        {
            throw new NotImplementedException();
        }

        private void setupInputFields(StackPanel mainPanel)
        {
            var fields = ParseInputFields(Properties.Settings.Default.InputFields);

            foreach (var group in fields)
            {
                group.AttachControls(mainPanel);
            }
        }

        /// <summary>
        /// Sets up the input fields from the configuration file
        /// </summary>
        /// <param name="inputFields"></param>
        /// <returns></returns>
        private List<InputGroup> ParseInputFields(StringCollection inputFields)
        {
            List<InputGroup> result = new List<InputGroup>();
            foreach (string field in inputFields)
            {
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace.StartsWith("PentaPrint.View.InputGroup") && !t.IsAbstract);
                foreach (var type in types)
                {
                    if (type.Name.Equals(field))
                    {
                        var instance = Activator.CreateInstance(type);
                        result.Add((InputGroup)instance);
                    }
                }
            }
            return result;
        }

    }
}
