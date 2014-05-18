using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModel
{

    /// <summary>参数块索引</summary>
    public enum ParamPartIndex
    {
        Basic = 0,
        Body,
        Engine,
        GearBox,
        UnderPanAndTurning,
        WheelsAndBraking,
        SafeEquipment,
        Control,
        Appearance,
        Internal,
        Seats,
        MultiMedia,
        Lights,
        Glasses,
        AirConditioner,
        HighTech
    }

    /// <summary>基本参数项</summary>
    public enum BasicItems
    {
        ModelName = 0,
        Price,
        Manufacturer,
        CarClass,
        Engine,
        GearBox,
        Size,
        BodyStruct,
        MaxSpeed,
        OfficalAccelerate,
        RealAccelerate,
        RealBraking,
        RealFuelConsumption,
        OfficalFuelConsumption,
        Warranty
    }

    /// <summary>车身参数项</summary>
    public enum BodyItems
    {
        Length = 0,
        Width,
        Height,
        Weight,
        WheelBase,
        FrontWheelTread,
        BackWheelTread,
        MinGroundClearance,
        StructType,
        DoorCount,
        SeatCount,
        FuelTankCapacity,
        CarBootCapacity
    }

    /// <summary>发动机参数项</summary>
    public enum EngineItems
    {
        Model = 0,
        Displacement,
        EngineType,
        CylinderArrangementType,
        CylinderCount,
        ValveCountPerCyliner,
        CompressionRatio,
        ValveGear,
        CylinderDiameter,
        Stroke,
        MaxHorsePower,
        MaxPower,
        SpeedAtMaxPower,
        MaxTorque,
        SpeedAtMaxTorque,
        SpecialEngineTech,
        FuelType,
        FuelLabel,
        FuelSupplyType,
        MaterialofCylinderCover,
        MaterialofCylinderBody,
        EnvironmentalStandard
    }

    /// <summary>变速箱参数项</summary>
    public enum GearBoxItems
    {
        BoxName,
        GearCount,
        BoxType
    }

    /// <summary>底盘转向参数项</summary>
    public enum UnderPanAndTurningItems
    {
        DriveMode,
        FrontSuspensionType,
        BackSuspensionType,
        PowerType,
        BodyStructure
    }

    /// <summary>底盘转向参数项</summary>
    public enum WheelsAndBrakingItems
    {
        FrontBrakingType,
        BackBrakingType,
        ParkBrakingType,
        FrontWheelSpecification,
        BackWheelSpecification,
        SpareWheelSpecification
    }

    //public struct CarSize
    //{
    //    public int Length { get; set; }
    //    public int Width { get; set; }
    //    public int Height { get; set; }

    //    /// <summary>轴距</summary>
    //    public int WheelBase { get; set;}

    //    /// <summary>前轮距</summary>
    //    public int FrontWheelTread { get; set; }

    //    /// <summary>后轮距</summary>
    //    public int BackWheelTread { get; set; }

    //    /// <summary>最小离地间隙</summary>
    //    public int MinGroundClearance { get; set; }

    //    public int Weight { get; set; }

    //}

    public enum EngineType
    {
        涡轮增压,
        自然吸气,
    }

    public enum StructType
    {
        两厢车,
        三厢车
    }
}
