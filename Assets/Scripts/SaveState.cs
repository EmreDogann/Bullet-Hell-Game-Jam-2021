// using System.IO;
// using System.Runtime.Serialization.Formatters.Binary;
// using UnityEngine;
//
// public static class SaveState{
//
//     public int[3] levels; 
//     
//     public static void SavePlayer (HealthStat player){
//
//         BinaryFormatter formatter = new BinaryFormatter();
//         string path = Application.persistentDataPath + "/player.saved";
//         FileStream stream = new FileStream(path, FileMode.Create);
//
//         PlayerData data = new PlayerData(player);
//
//         formatter.Serialize(stream, data);
//         stream.Close();
//
//     }
//
//     public static PlayerData LoadPlayer() {
//         string path = Application.persistentDataPath + "/player.saved";
//         if (File.Exists(path))
//         {
//             BinaryFormatter formatter = new BinaryFormatter();
//             FileStream stream = new FileStream(path, FileMode.Open);
//
//             PlayerData data = formatter.Deserialize(stream) as PlayerData;
//             stream.Close();
//
//             return data;
//         }
//         else
//         {
//             Debug.LogError("Save file not found" + path);
//             return null;
//         }
//     }
//
//     public static void SaveEnemies(SlimeController slime, Turret turret)
//     {
//
//         BinaryFormatter formatter = new BinaryFormatter();
//         string path = Application.persistentDataPath + "/enemy.saved" + "";
//         FileStream stream = new FileStream(path, FileMode.Create);
//
//         EnemyData data = new EnemyData(enemy);
//
//         formatter.Serialize(stream, data);
//         stream.Close();
//
//     }
// }
