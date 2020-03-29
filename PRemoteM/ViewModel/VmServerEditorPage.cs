﻿using System.Diagnostics;
using System.Linq;
using System.Windows;
using PRM.Core.Base;
using PRM.Core.DB;
using PRM.Core.Model;
using PRM.Core.Protocol.RDP;
using PRM.Core.UI.VM;
using PRM.RDP;
using PRM.View;
using Shawn.Ulits.PageHost;

namespace PRM.ViewModel
{
    public class VmServerEditorPage : NotifyPropertyChangedBase
    {
        private ServerAbstract _server = null;
        public ServerAbstract Server
        {
            get => _server;
            set => SetAndNotifyIfChanged(nameof(Server), ref _server, value);
        }

        public readonly VmServerListPage Host;

        public VmServerEditorPage(ServerAbstract server, VmServerListPage host)
        {
            Server = server;
            Host = host;
        }



        private RelayCommand _connServer;
        public RelayCommand ConnServer
        {
            get
            {
                if (_connServer == null)
                    _connServer = new RelayCommand((o) =>
                    {
                        
                        this.Server.Conn();
                    });
                return _connServer;
            }
        }



        private RelayCommand _cmdSave;
        public RelayCommand CmdSave
        {
            get
            {
                if (_cmdSave == null)
                    _cmdSave = new RelayCommand((o) =>
                    {
                        // edit
                        if (Server?.Id > 0 && Global.GetInstance().ServerList.Any(x=>x.Id == Server.Id))
                        {
                            Global.GetInstance().ServerList.First(x => x.Id == Server.Id).Update(Server);
                        }
                        // add
                        else
                        {
                            var serverOrm = ServerOrm.ConvertFrom(Server);
                            if (PRM_DAO.GetInstance().Insert(serverOrm))
                            {
                                var newServer = ServerFactory.GetInstance().CreateFromDb(serverOrm);
                                Global.GetInstance().ServerList.Add(newServer);
                            }
                        }
                        Host.Host.DispPage = null;
                    }, o => (this.Server.DispName != ""));
                return _cmdSave;
            }
        }




        private RelayCommand _cmdCancel;
        public RelayCommand CmdCancel
        {
            get
            {
                if (_cmdCancel == null)
                    _cmdCancel = new RelayCommand((o) =>
                    {
                        Host.Host.DispPage = null;
                    });
                return _cmdCancel;
            }
        }
    }
}
