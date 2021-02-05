using System.Collections;
using System.Collections.Generic;

public class GenericRoomDataModel
{
    public enum ROOM_TYPE
    {
        ENCOUNTER,
        SHOP,
        REST,


        NONE
    }

    //TODO: Change this to an enum for a database list of rooms at a later time, same with the connecting nodes
    protected int m_NodeID = -1;
    protected ROOM_TYPE m_RoomType = ROOM_TYPE.NONE;

    protected string m_BackgroundFilePath = "";
    protected string m_BackgroundFileName = "";

    protected int m_LeftConnectingNode = -1;
    protected int m_RightConnectingNode = -1;

    public int GetNodeID() 
    {
        return m_NodeID;
    }

    public void SetNodeID(int aNodeID) 
    {
        m_NodeID = aNodeID;
    }

    public ROOM_TYPE GetRoomType()
    {
        return m_RoomType;
    }

    public void SetRoomType(ROOM_TYPE aRoomType)
    {
        m_RoomType = aRoomType;
    }

    public string GetFilePath()
    {
        return m_BackgroundFilePath;
    }

    public void SetFilePath(string aFilePath)
    {
        m_BackgroundFilePath = aFilePath;
    }

    public string GetFileName()
    {
        return m_BackgroundFileName;
    }

    public void SetFileName(string aFileName)
    {
        m_BackgroundFileName = aFileName;
    }

    public int GetLeftConnectingNode()
    {
        return m_LeftConnectingNode;
    }

    public void SetLeftConnectingNode(int aNodeID) 
    {
        m_LeftConnectingNode = aNodeID;
    }

    public void SetRightConnectingNode(int aNodeID) 
    {
        m_RightConnectingNode = aNodeID;
    }

    public int GetRightConnectingNode() 
    {
        return m_RightConnectingNode;
    }
}
