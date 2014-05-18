using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace BusinessModel
{
    public abstract class CarParam
    {
        public ICollection<CarParam> collection;

        public string Name { get; set; }

        public abstract void AddChild(CarParam carParam) { }
    }

    public class CarConfig: CarParam
    {
        public string ParamValue { get; set; }
    }

    public class CarOption
    {
        public CarOptions ParamValue { get; set; }
    }

    public class CarParamGroup : CarParam { }

    public enum CarOptions
    {
        Yes = 0,
        No = 1,
        Customize = 2
    }

    public class CarModel
    {
        public CarModel()
        {

        }
    }
    
    public interface ICarParams
    {
        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <param name="paramValue">参数值</param>
        void SetParamValue(UInt16 paramIndex, string paramValue);
    }

    [Serializable]
    public class CarParams
    {
        public void SetParamValue(Int16 paramPartIndex, UInt16 paramItemIndex, string paramValue)
        {
            switch (paramPartIndex)
            {
                case (Int16)ParamPartIndex.Basic: this.basicParams.SetParamValue(paramItemIndex, paramValue); break;
                case (Int16)ParamPartIndex.Body: this.bodyParams.SetParamValue(paramItemIndex, paramValue); break;
                case (Int16)ParamPartIndex.Engine: this.engineParams.SetParamValue(paramItemIndex, paramValue); break;
                case (Int16)ParamPartIndex.GearBox:
                case (Int16)ParamPartIndex.UnderPanAndTurning:
                case (Int16)ParamPartIndex.WheelsAndBraking:
                case (Int16)ParamPartIndex.SafeEquipment:
                case (Int16)ParamPartIndex.Control:
                case (Int16)ParamPartIndex.Appearance:
                case (Int16)ParamPartIndex.Internal:
                case (Int16)ParamPartIndex.Seats:
                case (Int16)ParamPartIndex.MultiMedia:
                case (Int16)ParamPartIndex.Lights:
                case (Int16)ParamPartIndex.Glasses:
                case (Int16)ParamPartIndex.AirConditioner:
                case (Int16)ParamPartIndex.HighTech: break;

                default: throw new Exception("Wrong Param Index!");
            }

            ICarParams iParams;
            switch (paramPartIndex)
            {
                case (Int16)ParamPartIndex.Basic: iParams = basicParams; break;
                case (Int16)ParamPartIndex.Body: iParams = bodyParams; break;
                case (Int16)ParamPartIndex.Engine: iParams = engineParams; break;
                case (Int16)ParamPartIndex.GearBox: iParams = gearBoxParams; break;
                case (Int16)ParamPartIndex.UnderPanAndTurning: iParams = underPanAndTurningParams; break;
                case (Int16)ParamPartIndex.WheelsAndBraking: iParams = wheelsAndBraking; break;
                //case (Int16)ParamPartIndex.SafeEquipment:
                //case (Int16)ParamPartIndex.Control:
                //case (Int16)ParamPartIndex.Appearance:
                //case (Int16)ParamPartIndex.Internal:
                //case (Int16)ParamPartIndex.Seats:
                //case (Int16)ParamPartIndex.MultiMedia:
                //case (Int16)ParamPartIndex.Lights:
                //case (Int16)ParamPartIndex.Glasses:
                //case (Int16)ParamPartIndex.AirConditioner:
                //case (Int16)ParamPartIndex.HighTech: break;

                default: throw new Exception("Wrong Param Index!");
            }
            iParams.SetParamValue(paramItemIndex, paramValue);
        }

        private Basic basicParams = new Basic();
        public Basic BasicParams { get { return basicParams; } set { basicParams = value; } }

        private Body bodyParams = new Body();
        public Body BodyParams { get { return bodyParams; } set { bodyParams = value; } }

        private Engine engineParams = new Engine();
        public Engine EngineParams { get { return engineParams; } set { engineParams = value; } }

        private GearBox gearBoxParams = new GearBox();
        public GearBox GearBoxParams { get { return gearBoxParams; } set { gearBoxParams = value; } }

        private UnderPanAndTurning underPanAndTurningParams = new UnderPanAndTurning();
        public UnderPanAndTurning UnderPanAndTurningParams { get { return underPanAndTurningParams; } set { underPanAndTurningParams = value; } }

        private WheelsAndBraking wheelsAndBraking = new WheelsAndBraking();
        public WheelsAndBraking WheelsAndBraking { get { return wheelsAndBraking; } set { wheelsAndBraking = value; } }

        private SafeEquipment safeEquipment = new SafeEquipment();
        public SafeEquipment SafeEquipment { get { return safeEquipment; } set { safeEquipment = value; } }

        public void ToXml()
        {
            XmlSerializer xmls = new XmlSerializer(this.GetType());
            StreamWriter writer = new StreamWriter(@"D:\dsSerialize.xml");
            xmls.Serialize(writer, this);
            writer.Close();
        }
    }

    /// <summary>基本参数</summary>
    public class Basic : ICarParams
    {
        /// <summary>车型名称</summary>
        public string ModelName { get; set; }

        /// <summary>厂商指导价</summary>
        public string Price { get; set; }

        /// <summary>厂商</summary>
        public string Manufacturer { get; set; }

        /// <summary>级别</summary>
        public string CarClass { get; set; }

        /// <summary>发动机</summary>
        public string Engine { get; set; }

        /// <summary>变速箱</summary>
        public string GearBox { get; set; }

        /// <summary>长×宽×高(mm)</summary>
        public string Size { get; set; }

        /// <summary>车身结构</summary>
        public string BodyStruct { get; set; }

        /// <summary>最高车速(km/h)</summary>
        public string MaxSpeed { get; set; }

        /// <summary>官方0-100加速(s)</summary>
        public string OfficalAccelerate { get; set; }

        /// <summary>实测0-100加速(s)</summary>
        public string RealAccelerate { get; set; }

        /// <summary>实测100-0制动(m)</summary>
        public string RealBraking { get; set; }

        /// <summary>实测油耗(L)</summary>
        public string RealFuelConsumption { get; set; }

        /// <summary>工信部综合油耗(L)</summary>
        public string OfficalFuelConsumption { get; set; }

        /// <summary>整车质保</summary>
        public string Warranty { get; set; }

        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <param name="paramValue">参数值</param>
        public void SetParamValue(ushort paramIndex, string paramValue)
        {
            switch (paramIndex)
            {
                case (UInt16)BasicItems.ModelName: this.ModelName = paramValue; break;
                case (UInt16)BasicItems.Price: this.Price = paramValue; break;
                case (UInt16)BasicItems.Manufacturer: this.Manufacturer = paramValue; break;
                case (UInt16)BasicItems.CarClass: this.CarClass = paramValue; break;
                case (UInt16)BasicItems.Engine: this.Engine = paramValue; break;
                case (UInt16)BasicItems.GearBox: this.GearBox = paramValue; break;
                case (UInt16)BasicItems.Size: this.Size = paramValue; break;
                case (UInt16)BasicItems.BodyStruct: this.BodyStruct = paramValue; break;
                case (UInt16)BasicItems.MaxSpeed: this.MaxSpeed = paramValue; break;
                case (UInt16)BasicItems.OfficalAccelerate: this.OfficalAccelerate = paramValue; break;
                case (UInt16)BasicItems.RealAccelerate: this.RealAccelerate = paramValue; break;
                case (UInt16)BasicItems.RealBraking: this.RealBraking = paramValue; break;
                case (UInt16)BasicItems.RealFuelConsumption: this.RealFuelConsumption = paramValue; break;
                case (UInt16)BasicItems.OfficalFuelConsumption: this.OfficalFuelConsumption = paramValue; break;
                case (UInt16)BasicItems.Warranty: this.Warranty = paramValue; break;
                default: throw new Exception("Wrong Param Index in GearBox !");
            }
        }
    }

    /// <summary>车身参数</summary>
    public class Body : ICarParams
    {
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }

        /// <summary>轴距</summary>
        public string WheelBase { get; set; }

        /// <summary>前轮距</summary>
        public string FrontWheelTread { get; set; }

        /// <summary>后轮距</summary>
        public string BackWheelTread { get; set; }

        /// <summary>最小离地间隙</summary>
        public string MinGroundClearance { get; set; }

        /// <summary>车身结构</summary>
        public string StructType { get; set; }

        public string DoorCount { get; set; }

        public string SeatCount { get; set; }

        /// <summary>油箱容积</summary>
        public string FuelTankCapacity { get; set; }

        /// <summary>油箱容积</summary>
        public string CarBootCapacity { get; set; }

        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <param name="paramValue">参数值</param>
        public void SetParamValue(UInt16 paramIndex, string paramValue)
        {
            switch (paramIndex)
            {
                case (UInt16)BodyItems.Length: this.Length = paramValue; break;
                case (UInt16)BodyItems.Width: this.Width = paramValue; break;
                case (UInt16)BodyItems.Height: this.Height = paramValue; break;
                case (UInt16)BodyItems.Weight: this.Weight = paramValue; break;
                case (UInt16)BodyItems.WheelBase: this.WheelBase = paramValue; break;
                case (UInt16)BodyItems.FrontWheelTread: this.FrontWheelTread = paramValue; break;
                case (UInt16)BodyItems.BackWheelTread: this.BackWheelTread = paramValue; break;
                case (UInt16)BodyItems.MinGroundClearance: this.MinGroundClearance = paramValue; break;
                case (UInt16)BodyItems.StructType: this.StructType = paramValue; break;
                case (UInt16)BodyItems.DoorCount: this.DoorCount = paramValue; break;
                case (UInt16)BodyItems.SeatCount: this.SeatCount = paramValue; break;
                case (UInt16)BodyItems.FuelTankCapacity: this.FuelTankCapacity = paramValue; break;
                case (UInt16)BodyItems.CarBootCapacity: this.CarBootCapacity = paramValue; break;
                default: throw new Exception("Wrong Param Index in GearBox !");
            }
        }
    }

    /// <summary>发动机参数</summary>
    public class Engine : ICarParams
    {
        public string Model { get; set; }

        /// <summary>排量</summary>
        public string Displacement { get; set; }

        /// <summary>进气形式</summary>
        public string EngineType { get; set; }

        /// <summary>气缸排列形式</summary>
        public string CylinderArrangementType { get; set; }

        /// <summary>气缸数</summary>
        public string CylinderCount { get; set; }

        /// <summary>每缸气门数</summary>
        public string ValveCountPerCyliner { get; set; }

        /// <summary>压缩比</summary>
        public string CompressionRatio { get; set; }

        /// <summary>配气机构</summary>
        public string ValveGear { get; set; }

        /// <summary>缸径</summary>
        public string CylinderDiameter { get; set; }

        /// <summary>冲程</summary>
        public string Stroke { get; set; }

        /// <summary>最大马力</summary>
        public string MaxHorsePower { get; set; }

        /// <summary>最大功率</summary>
        public string MaxPower { get; set; }

        /// <summary>最大功率转速</summary>
        public string SpeedAtMaxPower { get; set; }

        /// <summary>最大扭矩</summary>
        public string MaxTorque { get; set; }

        /// <summary>最大扭矩转速</summary>
        public string SpeedAtMaxTorque { get; set; }

        /// <summary>发动机特有技术</summary>
        public string SpecialEngineTech { get; set; }

        /// <summary>燃料形式</summary>
        public string FuelType { get; set; }

        /// <summary>燃油标号</summary>
        public string FuelLabel { get; set; }

        /// <summary>供油方式</summary>
        public string FuelSupplyType { get; set; }

        /// <summary>缸盖材料</summary>
        public string MaterialofCylinderCover { get; set; }

        /// <summary>缸体材料</summary>
        public string MaterialofCylinderBody { get; set; }

        /// <summary>环保标准</summary>
        public string EnvironmentalStandard { get; set; }

        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <param name="paramValue">参数值</param>
        public void SetParamValue(UInt16 paramIndex, string paramValue)
        {
            switch (paramIndex)
            {
                case (UInt16)EngineItems.Model: this.Model = paramValue; break;
                case (UInt16)EngineItems.Displacement: this.Displacement = paramValue; break;
                case (UInt16)EngineItems.EngineType: this.EngineType = paramValue; break;
                case (UInt16)EngineItems.CylinderArrangementType: this.CylinderArrangementType = paramValue; break;
                case (UInt16)EngineItems.CylinderCount: this.CylinderCount = paramValue; break;
                case (UInt16)EngineItems.ValveCountPerCyliner: this.ValveCountPerCyliner = paramValue; break;
                case (UInt16)EngineItems.CompressionRatio: this.CompressionRatio = paramValue; break;
                case (UInt16)EngineItems.ValveGear: this.ValveGear = paramValue; break;
                case (UInt16)EngineItems.CylinderDiameter: this.CylinderDiameter = paramValue; break;
                case (UInt16)EngineItems.Stroke: this.Stroke = paramValue; break;
                case (UInt16)EngineItems.MaxHorsePower: this.MaxHorsePower = paramValue; break;
                case (UInt16)EngineItems.MaxPower: this.MaxPower = paramValue; break;
                case (UInt16)EngineItems.SpeedAtMaxPower: this.SpeedAtMaxPower = paramValue; break;
                case (UInt16)EngineItems.MaxTorque: this.MaxTorque = paramValue; break;
                case (UInt16)EngineItems.SpeedAtMaxTorque: this.SpeedAtMaxTorque = paramValue; break;
                case (UInt16)EngineItems.SpecialEngineTech: this.SpecialEngineTech = paramValue; break;
                case (UInt16)EngineItems.FuelType: this.FuelType = paramValue; break;
                case (UInt16)EngineItems.FuelLabel: this.FuelLabel = paramValue; break;
                case (UInt16)EngineItems.FuelSupplyType: this.FuelSupplyType = paramValue; break;
                case (UInt16)EngineItems.MaterialofCylinderCover: this.MaterialofCylinderCover = paramValue; break;
                case (UInt16)EngineItems.MaterialofCylinderBody: this.MaterialofCylinderBody = paramValue; break;
                case (UInt16)EngineItems.EnvironmentalStandard: this.EnvironmentalStandard = paramValue; break;

                default: throw new Exception("Wrong Param Index in GearBox !");
            }
        }
    }

    /// <summary>变速箱参数</summary>
    public class GearBox : ICarParams
    {
        /// <summary>简称</summary>
        public string BoxName { get; set; }

        /// <summary>档位数</summary>
        public string GearCount { get; set; }

        /// <summary>变速箱类型</summary>
        public string BoxType { get; set; }

        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <param name="paramValue">参数值</param>
        public void SetParamValue(UInt16 paramIndex, string paramValue)
        {
            switch (paramIndex)
            {
                case (UInt16)GearBoxItems.BoxName: this.BoxName = paramValue; break;
                case (UInt16)GearBoxItems.GearCount: this.GearCount = paramValue; break;
                case (UInt16)GearBoxItems.BoxType: this.BoxType = paramValue; break;
                default: throw new Exception("Wrong Param Index in GearBox !");
            }
        }
    }

    /// <summary>底盘转向参数</summary>
    public class UnderPanAndTurning : ICarParams
    {
        /// <summary>驱动方式</summary>
        public string DriveMode { get; set; }

        /// <summary>前悬挂类型</summary>
        public string FrontSuspensionType { get; set; }

        /// <summary>后悬挂类型</summary>
        public string BackSuspensionType { get; set; }

        /// <summary>助力类型</summary>
        public string PowerType { get; set; }

        /// <summary>车体结构</summary>
        public string BodyStructure { get; set; }

        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <param name="paramValue">参数值</param>
        public void SetParamValue(UInt16 paramIndex, string paramValue)
        {
            switch (paramIndex)
            {
                case (UInt16)UnderPanAndTurningItems.DriveMode: this.DriveMode = paramValue; break;
                case (UInt16)UnderPanAndTurningItems.FrontSuspensionType: this.FrontSuspensionType = paramValue; break;
                case (UInt16)UnderPanAndTurningItems.BackSuspensionType: this.BackSuspensionType = paramValue; break;
                case (UInt16)UnderPanAndTurningItems.PowerType: this.PowerType = paramValue; break;
                case (UInt16)UnderPanAndTurningItems.BodyStructure: this.BodyStructure = paramValue; break;
                default: throw new Exception("Wrong Param Index in UnderPanAndTurning !");
            }
        }
    }

    /// <summary>车轮制动参数</summary>
    public class WheelsAndBraking : ICarParams
    {
        /// <summary>前制动器类型</summary>
        public string FrontBrakingType { get; set; }

        /// <summary>后制动器类型</summary>
        public string BackBrakingType { get; set; }

        /// <summary>驻车制动类型</summary>
        public string ParkBrakingType { get; set; }

        /// <summary>前轮胎规格</summary>
        public string FrontWheelSpecification { get; set; }

        /// <summary>后轮胎规格</summary>
        public string BackWheelSpecification { get; set; }

        /// <summary>备胎规格</summary>
        public string SpareWheelSpecification { get; set; }

        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="paramIndex">参数索引</param>
        /// <param name="paramValue">参数值</param>
        public void SetParamValue(UInt16 paramIndex, string paramValue)
        {
            switch (paramIndex)
            {
                case (UInt16)WheelsAndBrakingItems.FrontBrakingType: this.FrontBrakingType = paramValue; break;
                case (UInt16)WheelsAndBrakingItems.BackBrakingType: this.BackBrakingType = paramValue; break;
                case (UInt16)WheelsAndBrakingItems.ParkBrakingType: this.ParkBrakingType = paramValue; break;
                case (UInt16)WheelsAndBrakingItems.FrontWheelSpecification: this.FrontWheelSpecification = paramValue; break;
                case (UInt16)WheelsAndBrakingItems.BackWheelSpecification: this.BackWheelSpecification = paramValue; break;
                case (UInt16)WheelsAndBrakingItems.SpareWheelSpecification: this.SpareWheelSpecification = paramValue; break;
                default: throw new Exception("Wrong Param Index in WheelsAndBraking !");
            }
        }
    }

    public class SafeEquipment
    {

    }

    public class Control
    {

    }

    public class Appearance
    {

    }

    public class Internal
    {

    }

    public class Seats
    {

    }

    public class MultiMedia
    {

    }

    public class Lights
    {

    }

    public class Glasses
    {

    }

    public class AirConditioner
    {

    }

    public class HighTech
    {

    }
}
