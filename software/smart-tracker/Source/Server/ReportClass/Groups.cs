using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Odbc;
using System.Data;

namespace AWI.SmartTracker.ReportClass
{
    [DataObject]
    public class Groups
    {
        private static readonly string ConnString = string.Format("DRIVER={{MySQL ODBC 3.51 Driver}};SERVER={0};DATABASE={1};USER={2};PASSWORD={3};OPTION=3;", MainForm.serverMySQL, MainForm.database, MainForm.user, MainForm.password);

        private static readonly string SelectAllCmd = "SELECT * FROM Groups ORDER BY Name";
        private static readonly string SelectCmd = "SELECT * FROM Groups WHERE GroupID=? LIMIT 1";
        private static readonly string InsertCmd = "INSERT INTO Groups (Name, Description) VALUES (?, ?)";
        private static readonly string UpdateCmd = "UPDATE Groups SET Name=?, Description=? WHERE GroupID=?";
        private static readonly string DeleteCmd = "DELETE Groups, GroupZones, GroupSchedules FROM Groups LEFT JOIN GroupZones LEFT JOIN GroupSchedules WHERE GroupID=?";


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Group> GetAllGroups()
        {
            var listGroup = new List<Group>();

            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(SelectAllCmd, con))
            {
                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        while (db.Read())
                        {
                            var group = new Group(Convert.ToInt32(db["GroupID"]),
                                                     db["Name"].ToString(),
                                                     db["Description"].ToString());
                            listGroup.Add(group);
                        }
                    }
                }
                catch
                {
                }
            }

            return listGroup;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Group GetGroup(int id)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(SelectCmd, con))
            {
                cmd.Parameters.AddWithValue("GroupID", id);

                try
                {
                    con.Open();
                    using (var db = cmd.ExecuteReader())
                    {
                        while (db.Read())
                        {
                            var group = new Group(Convert.ToInt32(db["GroupID"]),
                                                     db["Name"].ToString(),
                                                     db["Description"].ToString());
                            return group;
                        }
                    }
                }
                catch
                {
                }
            }

            return null;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertGroup(Group group)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(InsertCmd, con))
            {
                cmd.Parameters.AddWithValue("Name", group.Name);
                cmd.Parameters.AddWithValue("Description", group.Description);

                try
                {
                    con.Open();

                    OdbcTransaction transaction = con.BeginTransaction();

                    cmd.Connection = con;
                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT last_insert_id()";
                    var id = cmd.ExecuteScalar();

                    transaction.Commit();

                    group.ID = Convert.ToInt32(id);
                }
                catch
                {
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateGroup(Group group)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(UpdateCmd, con))
            {
                cmd.Parameters.AddWithValue("Name", group.Name);
                cmd.Parameters.AddWithValue("Description", group.Description);
                cmd.Parameters.AddWithValue("GroupID", group.ID);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void DeleteGroup(int id)
        {
            using (var con = new OdbcConnection(ConnString))
            using (var cmd = new OdbcCommand(DeleteCmd, con))
            {
                cmd.Parameters.AddWithValue("GroupID", id);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
            }
        }
    }

    public class Group
    {
        private int id;
        private string name;
        private string description;

        #region Constructors
        public Group()
        {
            id = -1;
            name = string.Empty;
            description = string.Empty;
        }

        public Group(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public Group(string name, string description)
        {
            this.id = -1;
            this.name = name;
            this.description = description;
        }
        #endregion

        #region Properties
        [DataObjectField(true)]
        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return description; } set { description = value; } }
        #endregion
    }
}
