using Cooking_App.Models;
using Cooking_WebApi.Models;
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
      
        public List<Receipe> GetAllProducts(string sname, string vnb)
        {
            var list = food.Receipes.ToList();
            List<Receipe> list1 = list.FindAll(x => x.State == sname && x.VNB == vnb);

            return list1;

        }
      
        public Receipe GetInfo(int id)
        {
            var list = food.Receipes.ToList();
            Receipe receipe = list.Find(x => x.RId == id);

            return receipe;
        }

        public List<State> GetAllState(string vnb)
        {
            List<String> list= food.Receipes.Where(x => x.VNB == vnb).Select(m => m.State).Distinct().ToList();
            List<State> slist = new List<State>();
            foreach (var item in list)
            {
                slist.Add(new State {Sname = item });
            }
            return slist;  
           
        }
        public List<Receipe> GetBeverageList(string vnb)
        {
            var list = food.Receipes.ToList();
            List<Receipe> list1 = list.FindAll(x => x.VNB == vnb);
            return list1;

        }

        //For Testing

        public bool Insert(Receipe m)
        {

            try
            {
                food.Receipes.Add(m);
                food.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        
        public bool RecipeExists(int id)
        {
            var list = food.Receipes.ToList();
            Receipe receipe = list.Find(x => x.RId == id);
            if (receipe !=null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var list = food.Receipes.ToList();
                Receipe receipe = list.Find(x => x.RId == id);

                food.Receipes.Remove(receipe);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Update(Receipe m)
        {
            try
            {
                var list = food.Receipes.ToList();
                Receipe found = list.Find(x => x.RId == m.RId);
                food.Receipes.Remove(found);
                food.Receipes.Add(m);
                food.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }    
        }
        
    }
}

