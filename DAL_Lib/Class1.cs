using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Lib
{
    public class LoginMethods
    {
        FoodReceipesEntities food = null;
        public LoginMethods()
        {
                food = new FoodReceipesEntities();
        }
       


        public int Feedback(string name, string email, string msg)
        {
            FeedBack f = new FeedBack();
            f.Name = name;
            f.Email = email;
            f.Msg = msg;

            try
            {
                food.FeedBacks.Add(f);
                food.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<FeedBack> FeedbackInfo()
        {
            var list = food.FeedBacks.ToList();
            List<FeedBack> list1 = (List<FeedBack>)list.GroupBy(x => x.Name).Take(3);
            return list1;
            //= new List<FeedBack>();
            //string str = "select Top 3 * from Feedback order by Fid desc";
            //cmd = new SqlCommand(str, con);
            //con.Open();
            //dr = cmd.ExecuteReader();
            //if (dr.HasRows)
            //{
            //    while (dr.Read())
            //    {
            //        Feedback f = new Feedback();
            //        f.Name = dr["Name"].ToString();
            //        f.Msg = dr["Message"].ToString();
            //        list.Add(f);
            //    }
            //    con.Close();
            //    return list;
            //}
            //else
            //{
            //    con.Close();
            //    return list;
            //}
        }
        public List<FeedBack> AllFeedbackInfo()
        {


            var list = food.FeedBacks.OrderByDescending(x => x.Email).ToList();
            return list;
        }


        public int Save(Login l)
        {
            int res;
            try
            {
                food.Logins.Add(l);
                food.SaveChanges();
                res = 1;
            }
            catch (Exception )
            {
                res= 0;
            }
            
            return res;
        }
        public int Search(string user, string pass)
        {
           
            var list = food.Logins.ToList();
            Login u=list.Find(x => x.Email == user);
            if (u!=null)
            {
                if (u.Password==pass)
                {
                    return 1;
                }
            }
            return 0;           
        }

        // Forget Password Query
        public int ForgetPassword(string email, DateTime date, string pass)
        {
            var list = food.Logins.ToList();
            Login u = list.Find(x => x.Email == email);
            if (u!=null)
            {
                if (u.DOB==date)
                {
                    u.Password = pass;
                    food.SaveChanges();
                    return 1;
                }

            }
            return 0; 
        }

        // Find Role Query
        public Login GetRole(string user, string pass)
        {

            var list = food.Logins.ToList();
            //Login l = new Login();
            //l.RoleID = list.Where(x => x.Email == user && x.Password == pass).Select(selector: x => x.RoleID);
            Login u = list.Find(x => x.Email == user && x.Password == pass);
            if (u != null)
            {
                if (u.Password == pass)
                {
                    return u;
                }
             }       
                return u;
           // return (int)l.RoleID;
        }

        // Find Name Query
        public Login GetName(string email, string pass)
        {
            var list = food.Logins.ToList();
            Login u = list.Find(x => x.Email == email);
            if (u != null)
            {
                if (u.Password == pass)
                {
                    return u;
                }
            }
            return u;
        }

        public int Temporary(string Email, string Password)
        {
            Logged l = new Logged();
            l.Name = Email;
            l.Password = Password;
            try
            {
                food.Loggeds.Add(l);
                food.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        // After Logged Query
        public int Temporaryvnb(string Email, string vnb)
        {
            int res = 0;
            try
            {
                var list = food.Loggeds.ToList();
                Logged l = list.Find(x => x.Name == Email);
                if (l != null)
                {
                    l.Name = Email;
                    l.Vnb = vnb;
                    food.SaveChanges();
                    res= 1;
                }
                
                
            }
            catch (Exception)
            {

                throw;
            }
            return res;
           

           
        }
        public int Temporarystate(string Email, string sname)
        {
            string str = "update Logged set sname=@sname where Username=@email";
            cmd = new SqlCommand(str, con);
            con.Open();
            cmd.Parameters.AddWithValue("@email", Email);
            cmd.Parameters.AddWithValue("@sname", sname);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public Logged TempName()
        {
            Logged log = new Logged();
            string str = "select * from Logged";
            cmd = new SqlCommand(str, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    log.Name = dr["Username"].ToString();
                    log.Password = dr["Password"].ToString();
                    log.Sname = dr["sname"].ToString();
                    log.Vnb = dr["vnb"].ToString();
                }
                con.Close();
                return log;
            }
            else
            {
                con.Close();
                return log;
            }
        }

        public void DeleteLogged()
        {
            
            string str = "delete from Logged";
            cmd = new SqlCommand(str, con);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
        }
    }

}
}
