using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppPlayerManagement.Models;

namespace WebAppPlayerManagement.Controllers
{
    public class PlayerController : Controller
    {
        string conString = ConfigurationManager.ConnectionStrings["PlayerConStr"].ConnectionString;
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader reader;
        // GET: Player
        public ActionResult Index()
        {
            List<Player> player = new List<Player>();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players");
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    player.Add(
                        new Player()
                        {
                            PlayerId = (int)(reader["PlayerId"]),
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            JerseyNumber = (int)reader["JerseyNumber"],
                            Position = (int)reader["Position"],
                            Team = (string)reader["Team"]
                        });
                }
            }
            catch (Exception ex)
            {
                TempData["error"]=ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }
            return View(player);
        }

        // GET: Player/Details/5
        public ActionResult Details(int id)
        {
            Player player = new Player();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players where PlayerId=@id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    player =
                        new Player()
                        {
                            PlayerId = (int)(reader["PlayerId"]),
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            JerseyNumber = (int)reader["JerseyNumber"],
                            Position = (int)reader["Position"],
                            Team = (string)reader["Team"]
                        };
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");

            }
            finally
            {
                con.Close();
            }
            return View(player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View(new Player());
        }

        // POST: Player/Create
        [HttpPost]
        public ActionResult Create(Player player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("insert into Players values(@pid,@fname,@lname,@jerseyNumber,@position,@team)");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@pid", player.PlayerId);
                cmd.Parameters.AddWithValue("@fname", player.FirstName);
                cmd.Parameters.AddWithValue("@lname", player.LastName);
                cmd.Parameters.AddWithValue("@jerseyNumber",player.JerseyNumber);
                cmd.Parameters.AddWithValue("@position", player.Position);
                cmd.Parameters.AddWithValue("@team", player.Team);
                con.Open();
                cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
            finally
            {
                con.Close() ;
            }
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Player/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Player player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("update Players set FirstName=@fname,LastName=@lname,JerseyNumber=@jerseyNumber,Position=@position,Team=@team where PlayerId=@pid");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@pid", id);
                cmd.Parameters.AddWithValue("@fname", player.FirstName);
                cmd.Parameters.AddWithValue("@lname", player.LastName);
                cmd.Parameters.AddWithValue("@jerseyNumber", player.JerseyNumber);
                cmd.Parameters.AddWithValue("@position", player.Position);
                cmd.Parameters.AddWithValue("@team", player.Team);
                con.Open();
                cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");

            }
            finally
            {
                con.Close();
            }
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int id)
        {
            Player player = new Player();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players where PlayerId=@id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    player =
                        new Player()
                        {
                            PlayerId = (int)(reader["PlayerId"]),
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            JerseyNumber = (int)reader["JerseyNumber"],
                            Position = (int)reader["Position"],
                            Team = (string)reader["Team"]
                        };
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");

            }
            finally
            {
                con.Close();
            }
            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Player player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("delete from Players where PlayerId=@id");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");

            }
            finally
            {
                con.Close();
            }
        }
    }
}
