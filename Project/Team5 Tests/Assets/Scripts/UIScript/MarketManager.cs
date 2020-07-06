using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public GameManager gameManager;
    List<BuildingData> buildingDataList;


    public void BuildtunnelEntrance()
    {
        gameManager.BuildStart(buildingDataList[0]);
        gameManager.BuyBuilding("tunnelEntrance");
    }
    public void BuildbaggageRoom()
    {
        gameManager.BuildStart(buildingDataList[1]);
        gameManager.BuyBuilding("baggageRoom");
    }
    public void Buildwindrad()
    {
        gameManager.BuildStart(buildingDataList[2]);
        gameManager.BuyBuilding("windrad");
    }
    public void BuildradioReceiver()
    {
        gameManager.BuildStart(buildingDataList[3]);
        gameManager.BuyBuilding("radioReceiver");
    }
    public void BuildApartment()
    {
        gameManager.BuildStart(buildingDataList[4]);
        gameManager.BuyBuilding("apartment");
    }
    public void BuildgeneralBuilding()
    {
        gameManager.BuildStart(buildingDataList[5]);
        gameManager.BuyBuilding("generalBuilding");
    }
    public void BuildmobileTypeBuilding()
    {
        gameManager.BuildStart(buildingDataList[6]);
        gameManager.BuyBuilding("mobileTypeBuilding");
    }
    public void Buildbridge()
    {
        gameManager.BuildStart(buildingDataList[7]);
        gameManager.BuyBuilding("bridge");
    }
    public void Buildtunnel()
    {
        gameManager.BuildStart(buildingDataList[8]);
        gameManager.BuyBuilding("tunnel");
    }
    public void Buildwaterway()
    {
        gameManager.BuildStart(buildingDataList[9]);
        gameManager.BuyBuilding("waterway");
    }
    public void Builddistrictt()
    {
        gameManager.BuildStart(buildingDataList[10]);
        gameManager.BuyBuilding("district");
    }
    public void Buildschool()
    {
        gameManager.BuildStart(buildingDataList[11]);
        gameManager.BuyBuilding("school");
    }
    public void Buildhospital()
    {
        gameManager.BuildStart(buildingDataList[12]);
        gameManager.BuyBuilding("hospital");
    }
    public void BuildprocessingFacility()
    {
        gameManager.BuildStart(buildingDataList[13]);
        gameManager.BuyBuilding("processingFacility");
    }
    public void Buildfactory()
    {
        gameManager.BuildStart(buildingDataList[14]);
        gameManager.BuyBuilding("factory");
    }
    public void Buildtower()
    {
        gameManager.BuildStart(buildingDataList[15]);
        gameManager.BuyBuilding("tower");
    }
    public void Buildsatelite1()
    {
        gameManager.BuildStart(buildingDataList[16]);
        gameManager.BuyBuilding("satelite1");
    }
    public void Buildsatelite2()
    {
        gameManager.BuildStart(buildingDataList[17]);
        gameManager.BuyBuilding("satelite2");
    }
    public void Buildpark()
    {
        gameManager.BuildStart(buildingDataList[18]);
        gameManager.BuyBuilding("park");
    }
    public void BuildpowerPlant()
    {
        gameManager.BuildStart(buildingDataList[19]);
        gameManager.BuyBuilding("powerPlant");
    }
    public void Buildgreenhouse()
    {
        gameManager.BuildStart(buildingDataList[20]);
        gameManager.BuyBuilding("greenhouse");
    }
    public void Buildfarm()
    {
        gameManager.BuildStart(buildingDataList[21]);
        gameManager.BuyBuilding("farm");
    }
    public void Buildport()
    {
        gameManager.BuildStart(buildingDataList[22]);
        gameManager.BuyBuilding("port");
    }
    public void Buildstadium()
    {
        gameManager.BuildStart(buildingDataList[23]);
        gameManager.BuyBuilding("stadium");
    }
    public void BuildspaceStation()
    {
        gameManager.BuildStart(buildingDataList[24]);
        gameManager.BuyBuilding("spaceStation");
    }
    public void Buildzone1Landmark()
    {
        gameManager.BuildStart(buildingDataList[25]);
        gameManager.BuyBuilding("zone1Landmark");
    }
    public void Buildzone2Landmark()
    {
        gameManager.BuildStart(buildingDataList[26]);
        gameManager.BuyBuilding("zone2Landmark");
    }
    public void Buildzone3Landmark()
    {
        gameManager.BuildStart(buildingDataList[27]);
        gameManager.BuyBuilding("zone3Landmark");
    }
    public void Buildplanet1()
    {
        gameManager.BuildStart(buildingDataList[28]);
        gameManager.BuyBuilding("planet1");
    }
    public void Buildplanet2()
    {
        gameManager.BuildStart(buildingDataList[29]);
        gameManager.BuyBuilding("planet2");
    }
    public void Buildplanet3()
    {
        gameManager.BuildStart(buildingDataList[30]);
        gameManager.BuyBuilding("planet3");
    }

}
