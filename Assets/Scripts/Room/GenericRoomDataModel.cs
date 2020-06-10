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

    protected ROOM_TYPE m_RoomType = ROOM_TYPE.NONE;

    protected string m_BackgroundFilePath = "";
    protected string m_BackgroundFileName = "";

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
}
