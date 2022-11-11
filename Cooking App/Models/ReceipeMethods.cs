using Cooking_App.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cooking_App
{
    public class ReceipeMethods
    {
        FoodReceipesEntities food = new FoodReceipesEntities();
        public int GetCountId()
        {
            return food.Logins.Count();
        }
        public int GetCountRId()
        {

            return food.Receipes.Count();
        }
        public int Insert(Receipe m)
        {
           
            try
            {
                food.Receipes.Add(m);
                food.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        public List<Receipe> GetAllProducts(string sname, string vnb)
        {
            var list = food.Receipes.ToList();
            List<Receipe> list1 = list.FindAll(x => x.State == sname && x.VNB == vnb);

            return list1;

        }
        public List<Receipe> GetAllProductsForModify(string sname, int id)
        {
            var list = food.Receipes.ToList();
            List<Receipe> list1 = list.FindAll(x => x.State == sname && x.UserId == id);
            return list1;
        }
        public Receipe GetInfo(int id)
        {
            var list = food.Receipes.ToList();
            Receipe receipe = list.Find(x => x.RId == id);

            return receipe;
        }

        public List<String> GetAllState(string vnb)
        {
            
            List<string> list= food.Receipes.Where(x => x.VNB == vnb).Select(m => m.State).Distinct().ToList();

            return list;
        }
        public List<Receipe> GetBeverageList(string vnb)
        {
            var list = food.Receipes.ToList();
            List<Receipe> list1 = list.FindAll(x => x.VNB == vnb);
            return list1;

        }

        public List<Receipe> GetBeverageListByUser(string vnb, int id)
        {

            var list = food.Receipes.ToList();
            List<Receipe> list1 = list.FindAll(x => x.VNB == vnb && x.UserId == id);
            return list1;


        }
        public int Delete(int id)
        {
            try
            {
                var list = food.Receipes.ToList();
                Receipe receipe = list.Find(x => x.RId == id);

                food.Receipes.Remove(receipe);
                return 1;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public int Update(Receipe m)
        {
            try
            {
                var list = food.Receipes.ToList();
                Receipe found = list.Find(x => x.RId == m.RId);
                found.RId = m.RId;
                found.RName = m.RName;
                found.Youtube = m.Youtube;
                found.Ingredient = m.Ingredient;
                found.HTM = m.HTM;
                found.Photo = m.Photo;
                found.VNB = m.VNB;
                found.State = m.State;

                food.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                throw;
            }    
        }
        public int UpdateWOPhoto(Receipe m)
        {
            try
            {
                var list = food.Receipes.ToList();
                Receipe found = list.Find(x => x.RId == m.RId);
                found.RId = m.RId;
                found.RName = m.RName;
                found.Youtube = m.Youtube;
                found.Ingredient = m.Ingredient;
                found.HTM = m.HTM;
                found.VNB = m.VNB;
                found.State = m.State;

                food.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

      
        //public Profile GetInfoProfile(int id)
        //{
        //    Profile m = new Profile();
        //    string str = "select * from Login where Id=@id";
        //    cmd = new SqlCommand(str, con);
        //    con.Open();
        //    cmd.Parameters.AddWithValue("@id", id);
        //    dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            m.Id = Convert.ToInt32(dr["Id"]);
        //            m.Username = dr["Username"].ToString();
        //            m.Email = dr["Email"].ToString();
        //            m.Password = dr["Password"].ToString();
        //            m.RoleId = Convert.ToInt32(dr["RoleID"]);
        //            m.DOB = dr["DOB"].ToString();
        //            m.Gender = dr["Gender"].ToString();
        //            m.Profession = dr["Profession"].ToString();
        //            m.City = dr["City"].ToString();
        //            m.PhotoName = dr["ProfilePhoto"].ToString();
        //        }
        //        con.Close();
        //        return m;
        //    }
        //    return m;
        //}
      
        //public int UpdateProfilePhoto(Profile p)
        //{
        //    string str = "update Login set Username=@username,Email=@email,Password=@pass,Profession=@profession,City=@city,DOB=@dob,ProfilePhoto=@pname,Gender=@gender where Id=@id";
        //    cmd = new SqlCommand(str, con);
        //    con.Open();
        //    cmd.Parameters.AddWithValue("@username", p.Username);
        //    cmd.Parameters.AddWithValue("@email", p.Email);
        //    cmd.Parameters.AddWithValue("@pass", p.Password);
        //    cmd.Parameters.AddWithValue("@profession", p.Profession);
        //    cmd.Parameters.AddWithValue("@city", p.City);
        //    cmd.Parameters.AddWithValue("@dob", p.DOB);
        //    cmd.Parameters.AddWithValue("@pname", p.PhotoName);
        //    cmd.Parameters.AddWithValue("@gender", p.Gender);
        //    cmd.Parameters.AddWithValue("@id", p.Id);
        //    int res = cmd.ExecuteNonQuery();
        //    con.Close();
        //    return res;
        //}
     
        //public int UpdateProfile(Profile p)
        //{
        //    string str = "update Login set Username=@username,Email=@email,Password=@pass,Profession=@profession,City=@city,DOB=@dob,Gender=@gender where Id=@id";
        //    cmd = new SqlCommand(str, con);
        //    con.Open();
        //    cmd.Parameters.AddWithValue("@username", p.Username);
        //    cmd.Parameters.AddWithValue("@email", p.Email);
        //    cmd.Parameters.AddWithValue("@pass", p.Password);
        //    cmd.Parameters.AddWithValue("@profession", p.Profession);
        //    cmd.Parameters.AddWithValue("@city", p.City);
        //    cmd.Parameters.AddWithValue("@dob", p.DOB);
        //    cmd.Parameters.AddWithValue("@gender", p.Gender);
        //    cmd.Parameters.AddWithValue("@id", p.Id);
        //    int res = cmd.ExecuteNonQuery();
        //    con.Close();
        //    return res;
        //}
    }
}

