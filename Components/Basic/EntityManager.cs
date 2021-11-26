using System;
using System.Collections.Generic;
using Godot;


public class EntityManager
{
    private static Dictionary<string, CommonEntity> entities = new Dictionary<string, CommonEntity>();

    public static void AddEntity(string name, CommonEntity entity)
    {
        entities.Add(name, entity);
    }

    public static void Restart()
    {
        entities.Clear();
    }

    public static void RemoveEntity(string name)
    {
        entities.Remove(name);
    }

    public static CommonEntity returnEntity(string tag)
    {
        try
        {
            return entities[tag];
        }
        catch(Exception exception)
        {
            GD.Print(exception.Message);
        }

        return null;
    }
}