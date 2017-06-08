using ProjetoWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoWeb.Repositorio
{
    public class RelatorioRepositorio
    {
 
        public List<TotalVenda> VendasPorDia(string ano, string mes)
        {
            /* CONFIGURAÇÃO LOCAL
             *
             * /string connectionString = "Server=DESKTOP-EL880BB;Database=ProjetoWeb;Trusted_Connection=True;";
            */

            /* CONFIGURAÇÃO REMOTO
            *
           string connectionString = "Server=SQLXXXX.SmarterASP.NET;Database=DB_XXXXX_ProjetoWeb;User Id=DB_XXXXXXX_ProjetoWeb_admin;Password=XXXXXXXXXXXXXX;";
           */
           
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = @"select 
                                    day(DataVenda) Dia, 
	                                sum(TotalVenda) Total
                                from Venda
                                where month(DataVenda) = @Mes
                                and year(DataVenda) = @Ano
                                group by day(DataVenda)";
            cmd.Parameters.AddWithValue("Mes", mes);
            cmd.Parameters.AddWithValue("Ano", ano);

            SqlDataReader dr = cmd.ExecuteReader();

            List<TotalVenda> totais = new List<TotalVenda>();

            while (dr.Read())
            {
                TotalVenda total = new TotalVenda();
                total.Dia = dr["DIA"].ToString();
                total.ValorTotal = Convert.ToDecimal(dr["TOTAL"]);
                totais.Add(total);
            }

            return totais;

        }
    }
}