using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mabi_Inventory_Manager
{
    class Item
    {
        public long EntityID { get; set; } = 0;
        public byte itemType { get; set; } = 0;
        // ItemInfo
        public byte Pocket { get; set; } = 0;
        public byte Unknown1 { get; set; } = 0;
        public byte Unknown2 { get; set; } = 0;
        public byte Unknown3 { get; set; } = 0;
        public int Id { get; set; } = 0;
        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public Color Color3 { get; set; }
        public ushort Amount { get; set; } = 0;
        public int Region { get; set; } = 0;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public byte State { get; set; } = 0;
        public byte FigureB { get; set; } = 0;
        public byte FigureC { get; set; } = 0;
        public byte FigureD { get; set; } = 0;
        public byte KnockCount { get; set; } = 0;
        public byte Unknown4 { get; set; } = 0;
        public byte Unknown5 { get; set; } = 0;
        public byte Unknown6 { get; set; } = 0;
        public byte Unknown7 { get; set; } = 0;
        public byte Unknown8 { get; set; } = 0;
        public byte Unknown9 { get; set; } = 0;
        public byte Unknown10 { get; set; } = 0;
        // ItemOptionInfo
        public byte Flag { get; set; } = 0;
        public byte Unknown11 { get; set; } = 0;
        public byte Unknown12 { get; set; } = 0;
        public byte Unknown13 { get; set; } = 0;
        public int Price { get; set; } = 0;
        public int SellingPrice { get; set; } = 0;
        public int LinkedPocketId { get; set; } = 0;
        public int Durability { get; set; } = 0;
        public int DurabilityMax { get; set; } = 0;
        public int DurabilityOriginal { get; set; } = 0;
        public ushort AttackMin { get; set; } = 0;
        public ushort AttackMax { get; set; } = 0;
        public ushort InjuryMin { get; set; } = 0;
        public ushort InjuryMax { get; set; } = 0;
        public byte Balance { get; set; } = 0;
        public sbyte Critical { get; set; } = 0;
        public byte Unknown14 { get; set; } = 0;
        public byte Unknown15 { get; set; } = 0;
        public int Defense { get; set; } = 0;
        public short Protection { get; set; } = 0;
        public short EffectiveRange { get; set; } = 0;
        public byte AttackSpeed { get; set; } = 0;
        public byte KnockCount2 { get; set; } = 0;
        public short Experience { get; set; } = 0;
        public byte EP { get; set; } = 0;
        public byte Upgraded { get; set; } = 0;
        public byte UpgradeMax { get; set; } = 0;
        public byte WeaponType { get; set; } = 0;
        public int Grade { get; set; } = 0;
        public ushort Prefix { get; set; } = 0;
        public ushort Suffix { get; set; } = 0;
        public short Elemental { get; set; } = 0;
        public short Unknown16 { get; set; } = 0;
        public int ExpireTime { get; set; } = 0;
        public int StackRemainingTime { get; set; } = 0;
        public int JoustPointPrice { get; set; } = 0;
        public int DucatPrice { get; set; } = 0;
        public int StarPrice { get; set; } = 0;
        public int PointPrice { get; set; } = 0;
        public int Unknown17 { get; set; } = 0;
        public int Unknown18 { get; set; } = 0;
        public string MetaData1 { get; set; } = "";
        public string MetaData2 { get; set; } = "";
        public byte UpgradeNum { get; set; } = 0;
        //public Upgrade[] Upgrades { get; set; }
        public byte[][] Upgrades { get; set; }
        public long QuestID { get; set; } = 0;
        public byte IsNew { get; set; } = 0;
        public byte EndByte { get; set; } = 0;
        public string ItemName { get; set; } = "";

        public void setItemInfo(byte[] data)
        {
            int current = 0;
            var pocketBin = Parser.GetNextRev(data, ref current, 1);
            this.Pocket = Parser.ConvertByte(pocketBin);
            var unknown1Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown1 = Parser.ConvertByte(unknown1Bin);
            var unknown2Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown2 = Parser.ConvertByte(unknown2Bin);
            var unknown3Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown3 = Parser.ConvertByte(unknown3Bin);
            var idBin = Parser.GetNextRev(data, ref current, 4);
            this.Id = Parser.ConvertInt(idBin);
            var color1Bin = Parser.GetNextRev(data, ref current, 4);
            this.Color1 = Parser.ConvertColor(color1Bin);
            var color2Bin = Parser.GetNextRev(data, ref current, 4);
            this.Color2 = Parser.ConvertColor(color2Bin);
            var color3Bin = Parser.GetNextRev(data, ref current, 4);
            this.Color3 = Parser.ConvertColor(color3Bin);
            var amountBin = Parser.GetNextRev(data, ref current, 2);
            this.Amount = Parser.ConvertUShort(amountBin);
            var regionBin = Parser.GetNextRev(data, ref current, 4);
            this.Region = Parser.ConvertInt(regionBin);
            var XBin = Parser.GetNextRev(data, ref current, 4);
            this.X = Parser.ConvertInt(XBin);
            var YBin = Parser.GetNextRev(data, ref current, 4);
            this.Y = Parser.ConvertInt(YBin);
            var stateBin = Parser.GetNextRev(data, ref current, 1);
            this.State = Parser.ConvertByte(stateBin);
            var FigureBBin = Parser.GetNextRev(data, ref current, 1);
            this.FigureB = Parser.ConvertByte(FigureBBin);
            var FigureCBin = Parser.GetNextRev(data, ref current, 1);
            this.FigureC = Parser.ConvertByte(FigureCBin);
            var FigureDBin = Parser.GetNextRev(data, ref current, 1);
            this.FigureD = Parser.ConvertByte(FigureDBin);
            var KnockCountBin = Parser.GetNextRev(data, ref current, 1);
            this.KnockCount = Parser.ConvertByte(KnockCountBin);
            var Unknown4Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown4 = Parser.ConvertByte(Unknown4Bin);
            var Unknown5Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown5 = Parser.ConvertByte(Unknown5Bin);
            var Unknown6Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown6 = Parser.ConvertByte(Unknown6Bin);
            var Unknown7Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown7 = Parser.ConvertByte(Unknown7Bin);
            var Unknown8Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown8 = Parser.ConvertByte(Unknown8Bin);
            var Unknown9Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown9 = Parser.ConvertByte(Unknown9Bin);
            var Unknown10Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown10 = Parser.ConvertByte(Unknown10Bin);
        }

        public void setItemOptionInfo(byte[] data)
        {
            int current = 0;
            var flagBin = Parser.GetNextRev(data, ref current, 1);
            this.Flag = Parser.ConvertByte(flagBin);
            var unknown11Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown11 = Parser.ConvertByte(unknown11Bin);
            var unknown12Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown12 = Parser.ConvertByte(unknown12Bin);
            var unknown13Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown13 = Parser.ConvertByte(unknown13Bin);
            var priceBin = Parser.GetNextRev(data, ref current, 4);
            this.Price = Parser.ConvertInt(priceBin);
            var sellingPriceBin = Parser.GetNextRev(data, ref current, 4);
            this.SellingPrice = Parser.ConvertInt(sellingPriceBin);
            var linkedPocketIdBin = Parser.GetNextRev(data, ref current, 4);
            this.LinkedPocketId = Parser.ConvertInt(linkedPocketIdBin);
            var durabilityBin = Parser.GetNextRev(data, ref current, 4);
            this.Durability = Parser.ConvertInt(durabilityBin);
            var durabilityMaxBin = Parser.GetNextRev(data, ref current, 4);
            this.DurabilityMax = Parser.ConvertInt(durabilityMaxBin);
            var durabilityOriginalBin = Parser.GetNextRev(data, ref current, 4);
            this.DurabilityOriginal = Parser.ConvertInt(durabilityOriginalBin);
            var attackMinBin = Parser.GetNextRev(data, ref current, 2);
            this.AttackMin = Parser.ConvertUShort(attackMinBin);
            var attackMaxBin = Parser.GetNextRev(data, ref current, 2);
            this.AttackMax = Parser.ConvertUShort(attackMaxBin);
            var injuryMinBin = Parser.GetNextRev(data, ref current, 2);
            this.InjuryMin = Parser.ConvertUShort(injuryMinBin);
            var injuryMaxBin = Parser.GetNextRev(data, ref current, 2);
            this.InjuryMax = Parser.ConvertUShort(injuryMaxBin);
            var balanceBin = Parser.GetNextRev(data, ref current, 1);
            this.Balance = Parser.ConvertByte(balanceBin);
            var criticalBin = Parser.GetNextRev(data, ref current, 1);
            this.Critical = Parser.ConvertSByte(criticalBin);
            var unknown14Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown14 = Parser.ConvertByte(unknown14Bin);
            var unknown15Bin = Parser.GetNextRev(data, ref current, 1);
            this.Unknown15 = Parser.ConvertByte(unknown15Bin);
            var defenseBin = Parser.GetNextRev(data, ref current, 4);
            this.Defense = Parser.ConvertInt(defenseBin);
            var protectionBin = Parser.GetNextRev(data, ref current, 2);
            this.Protection = Parser.ConvertShort(protectionBin);
            var effectiveRangeBin = Parser.GetNextRev(data, ref current, 2);
            this.EffectiveRange = Parser.ConvertShort(effectiveRangeBin);
            var attackSpeedBin = Parser.GetNextRev(data, ref current, 1);
            this.AttackSpeed = Parser.ConvertByte(attackSpeedBin);
            var knockCount2Bin = Parser.GetNextRev(data, ref current, 1);
            this.KnockCount2 = Parser.ConvertByte(knockCount2Bin);
            var experienceBin = Parser.GetNextRev(data, ref current, 2);
            this.Experience = Parser.ConvertShort(experienceBin);
            var epBin = Parser.GetNextRev(data, ref current, 1);
            this.EP = Parser.ConvertByte(epBin);
            var upgradedBin = Parser.GetNextRev(data, ref current, 1);
            this.Upgraded = Parser.ConvertByte(upgradedBin);
            var upgradeMaxBin = Parser.GetNextRev(data, ref current, 1);
            this.UpgradeMax = Parser.ConvertByte(upgradeMaxBin);
            var weaponTypeBin = Parser.GetNextRev(data, ref current, 1);
            this.WeaponType = Parser.ConvertByte(weaponTypeBin);
            var gradeBin = Parser.GetNextRev(data, ref current, 4);
            this.Grade = Parser.ConvertInt(gradeBin);
            var prefixBin = Parser.GetNextRev(data, ref current, 2);
            this.Prefix = Parser.ConvertUShort(prefixBin);
            var suffixBin = Parser.GetNextRev(data, ref current, 2);
            this.Suffix = Parser.ConvertUShort(suffixBin);
            var elementalBin = Parser.GetNextRev(data, ref current, 2);
            this.Elemental = Parser.ConvertShort(elementalBin);
            var unknown16Bin = Parser.GetNextRev(data, ref current, 2);
            this.Unknown16 = Parser.ConvertShort(unknown16Bin);
            var expireTimeBin = Parser.GetNextRev(data, ref current, 4);
            this.ExpireTime = Parser.ConvertInt(expireTimeBin);
            var stackRemainingTimeBin = Parser.GetNextRev(data, ref current, 4);
            this.StackRemainingTime = Parser.ConvertInt(stackRemainingTimeBin);
            var joustPointPriceBin = Parser.GetNextRev(data, ref current, 4);
            this.JoustPointPrice = Parser.ConvertInt(joustPointPriceBin);
            var ducatPriceBin = Parser.GetNextRev(data, ref current, 4);
            this.DucatPrice = Parser.ConvertInt(ducatPriceBin);
            var starPriceBin = Parser.GetNextRev(data, ref current, 4);
            this.StarPrice = Parser.ConvertInt(starPriceBin);
            var pointPriceBin = Parser.GetNextRev(data, ref current, 4);
            this.PointPrice = Parser.ConvertInt(pointPriceBin);
            var unknown17Bin = Parser.GetNextRev(data, ref current, 4);
            this.Unknown17 = Parser.ConvertInt(unknown17Bin);
            var unknown18Bin = Parser.GetNextRev(data, ref current, 4);
            this.Unknown18 = Parser.ConvertInt(unknown18Bin);
        }

        public void setUpgradeSize()
        {
            this.Upgrades = new byte[this.UpgradeNum][];
        }

        public string GetHeader()
        {
            string str = "";
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this))
            {
                //Console.WriteLine(String.Format("{0} {1}", descriptor.Name, descriptor.GetValue(this)));
                str += "\"" + descriptor.Name.ToString() + "\",";
            }
            return str;
        }

        public override string ToString()
        {
            string str = "";

            
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this))
            {
                //Console.WriteLine(String.Format("{0} {1}", descriptor.Name, descriptor.GetValue(this)));
                str += "\"" + descriptor.GetValue(this).ToString() + "\",";
            }
            
            //str = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},", this.EntityID, this.ItemName, this.Pocket, this.Color1, this.Color2, this.Color3, this.X, this.Y, this.Amount);
            return str;
        }
    }

    class Upgrade
    {
        public override string ToString()
        {
            return "";
        }
    }


}
