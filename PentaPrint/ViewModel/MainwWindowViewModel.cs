﻿using PentaPrint.Commands;
using PentaPrint.Devices;
using PentaPrint.Mediator;
using PentaPrint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PentaPrint.ViewModel
{
    class MainwWindowViewModel
    {
        PrintMediator printMediator = PrintMediator.Instance;
        public CommandGroup PrintAll { get; private set; }
        public PrintCommand PrintOne { get; private set; }
        public ICommand OpenDialog { get; private set; }

        public MainwWindowViewModel()
        {
            PrintAll = new CommandGroup();
            PrintAll.Commands.Add(new PrintAll(printMediator));
            PrintAll.Commands.Add(new ResetAll(printMediator));


            PrintOne = new PrintCommand(printMediator);

            OpenDialog = new OpenDialog();
        }
        
    }
}
