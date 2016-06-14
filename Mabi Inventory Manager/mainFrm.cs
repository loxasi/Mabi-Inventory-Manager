using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Windows.Interop;
using System.IO;

namespace Mabi_Inventory_Manager
{

    public partial class mainFrm : Form
    {
        public IntPtr AlissaHandle { get; private set; }
        public System.IO.StreamWriter packet_f;

        private const string packets_path = @"packets.txt";
        private const string char_packet_path = @"char_packet.txt";
        private const string inventory_path = @"inventory.csv";
        private const string inventorysimp_path = @"inventorysimp.csv";

        public mainFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            var alissaWindows = WinApi.FindAllWindows("mod_Alissa");

            if (alissaWindows.Count == 0)
            {
                System.Windows.MessageBox.Show("No packet provider found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            packet_f = new System.IO.StreamWriter(packets_path);

            AlissaHandle = alissaWindows[0].HWnd;
            SendAlissa(AlissaHandle, 100);
            packet_f.WriteLine("Attached to packet provider.");
        }

        private void SendAlissa(IntPtr hWnd, int op)
        {
            WinApi.COPYDATASTRUCT cds;
            cds.dwData = (IntPtr)op;
            cds.cbData = 0;
            cds.lpData = IntPtr.Zero;

            var cdsBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(cds));
            Marshal.StructureToPtr(cds, cdsBuffer, false);

            //Dispatcher.Invoke(delegate
            //{
            WinApi.SendMessage(hWnd, WinApi.WM_COPYDATA, this.Handle, cdsBuffer);
            //});
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            SendAlissa(AlissaHandle, 101);
            packet_f.Close();
            this.Close();
        }
        
        //public override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WinApi.WM_COPYDATA)
            {
                var cds = (WinApi.COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(WinApi.COPYDATASTRUCT));

                string type = "INC";

                if (cds.cbData < 12)
                    return;

                var recv = (int)cds.dwData == 0x10101012;

                if (!recv) {
                    type = "OUT";
                }
                var data = new byte[cds.cbData];
                Marshal.Copy(cds.lpData, data, 0, cds.cbData);

                var packet = new Packet(data, 0);

                //System.Windows.MessageBox.Show(packet.Op.ToString(), "", 0, 0);
                packet_f.WriteLine(String.Format("{0,10}  {1}  {2}", packet.Op.ToString("X"), type, BitConverter.ToString(packet.Build()).Replace("-", "")));
                // ChannelCharacterInfoRequestR packet
                if (packet.Op == 0x5209) {
                    handleChar(packet);
                }
                return;
            }
            base.WndProc(ref m);
        }

        private int findSequence(byte[] charBin, byte[] search) {
            // current sequence to compare
            var current = new byte[10];
            // search until the last comparable sequence
            var maxSearchRange = charBin.Length - 9;
            // loop through each sequence in order
            for (var i = 0; i < maxSearchRange; i++) {
                for (var j = 0; j < 10; j++)
                    current[j] = charBin[i+j];
                // if sequences match, return the start index of the sequence
                if ((search).SequenceEqual(current))
                    return i;
            }
            return -1;
        }


        private void handleChar(Packet packet)
        {
            Console.WriteLine("handleChar(): entering");
            var packetData = packet.Build();
            using (System.IO.StreamWriter char_packet_f = new System.IO.StreamWriter(char_packet_path))
                char_packet_f.WriteLine(BitConverter.ToString(packetData).Replace("-", " "));

            // start of inventory data
            // size of player's inventory 6x10
            var invSize = new byte[] { 0x03, 0x00, 0x00, 0x00, 0x06, 0x03, 0x00, 0x00, 0x00, 0x0A };
            // locate start of inventory data
            var current = findSequence(packetData, invSize);
            current += 10;
            // get number of items
            var itemNumBin = Parser.GetNext(packetData, ref current);
            int itemNum = Parser.ConvertInt(itemNumBin);

            using (System.IO.StreamWriter inventory_f = new System.IO.StreamWriter(inventory_path))
            using (System.IO.StreamWriter inventorysimp_f = new System.IO.StreamWriter(inventorysimp_path))
            {
                // print csv header
                Item header = new Item();
                inventory_f.WriteLine(header.GetHeader());
                inventorysimp_f.WriteLine("Pocket,Name,Amount,Durability");

                // loop through items
                for (var i = 0; i < itemNum; i++)
                {
                    Console.WriteLine("Processing item {0} of {1}...", i, itemNum);
                    Item newItem = new Item();
                    var entityIdBin = Parser.GetNext(packetData, ref current);
                    newItem.EntityID = Parser.ConvertLong(entityIdBin);
                    var itemTypeBin = Parser.GetNext(packetData, ref current);
                    newItem.itemType = Parser.ConvertByte(itemTypeBin);
                    var itemInfoBin = Parser.GetNext(packetData, ref current);
                    newItem.setItemInfo(itemInfoBin);
                    // Handle extra string for Brionac
                    if (newItem.Id == 40319)
                    {
                        var extra = Parser.GetNext(packetData, ref current);
                    }
                    var itemOptionInfoBin = Parser.GetNext(packetData, ref current);
                    newItem.setItemOptionInfo(itemOptionInfoBin);
                    // ego info
                    // get itemdb info of item
                    var itemDBInfo = ItemDB.ID(newItem.Id);
                    // check if item is an ego
                    if (itemDBInfo.Item2.Contains("/ego_weapon/"))
                    {
                        for (var j = 0; j < 20; j++)
                        {
                            // do things with ego data
                            var ego = Parser.GetNext(packetData, ref current);
                        }
                    }
                    var metaData1Bin = Parser.GetNext(packetData, ref current);
                    newItem.MetaData1 = Parser.ConvertString(metaData1Bin);
                    var metaData2Bin = Parser.GetNext(packetData, ref current);
                    newItem.MetaData2 = Parser.ConvertString(metaData2Bin);

                    var upgradeNumBin = Parser.GetNext(packetData, ref current);
                    newItem.UpgradeNum = Parser.ConvertByte(upgradeNumBin);
                    newItem.setUpgradeSize();
                    for (var j = 0; j < newItem.UpgradeNum; j++)
                    {
                        var upgradeBin = Parser.GetNext(packetData, ref current);
                        newItem.Upgrades[j] = upgradeBin;
                    }
                    var questIDBin = Parser.GetNext(packetData, ref current);
                    newItem.QuestID = Parser.ConvertLong(questIDBin);

                    // don't know what this info is
                    // special upgrade?
                    var test = Parser.GetNext(packetData, ref current);
                    while (test.Length == 4)
                    {
                        var testN = Parser.ConvertInt(test);
                        Console.WriteLine(String.Format("{0} {1}", newItem.EntityID, testN));
                        test = Parser.GetNext(packetData, ref current);
                    }

                    var isNewBin = test;
                    newItem.IsNew = Parser.ConvertByte(isNewBin);
                    var endByteBin = Parser.GetNext(packetData, ref current);
                    newItem.EndByte = Parser.ConvertByte(endByteBin);
                    newItem.ItemName = itemDBInfo.Item1;
                    // write item data
                    inventory_f.WriteLine(newItem);
                    inventorysimp_f.WriteLine("{0},{1},{2},{3}", newItem.Pocket, newItem.ItemName, newItem.Amount, ((float)newItem.Durability) / 1000);
                }
            }
        }

        private static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private void fromFileBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Processing character info packet from previously saved file.");
            byte[] bytes = StringToByteArray(File.ReadAllText(char_packet_path).Replace(" ", "").Replace("\r\n", ""));
            Packet p = new Packet(bytes, 0);
            handleChar(p);
        }

        private void chooseDirBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            if(d.ShowDialog() == DialogResult.OK)
                Directory.SetCurrentDirectory(d.SelectedPath);
        }
    }
}

