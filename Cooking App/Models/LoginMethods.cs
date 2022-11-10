﻿using Cooking_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooking_App
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
            return list;
            //= new List<FeedBack>();
            //string str = "select Top 3 * from Feedback order by Fid desc";
            
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
                Logged l = list.First(x => x.Name == Email);
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

            int res = 0;
            try
            {
                var list = food.Loggeds.ToList();
                Logged l = list.First(x => x.Name == Email);
                if (l != null)
                {

                    l.Name = Email;
                    l.Sname = sname;

                    food.SaveChanges();
                    res = 1;
                }


            }
            catch (Exception)
            {

                throw;
            }
            return res;
            
           
        }
        public Logged TempName()
        {
          
            try
            {
                return food.Loggeds.First();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeleteLogged()
        {
            food.Loggeds.RemoveRange(food.Loggeds);

        }
    }

}
