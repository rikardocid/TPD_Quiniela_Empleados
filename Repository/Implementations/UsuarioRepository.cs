using ASPNetCore6Identity.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;
using System;
using System.Data;

namespace Quinela_TPD.Repository.Implementations
{
    public class UsuarioRepository : GenericRepository<UsuarioModel>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public void GetExtraerCodigosPromocionales()
        {

            //Declaracion de la variable donde se almacena el query 
            DataTable resultQuery = new DataTable();
            //Variable de conexion
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                //Se conecta a la base de datos, al return se cierra la conexion
                sqlcon.ConnectionString = _context.Database.GetDbConnection().ConnectionString;

                SqlCommand SqlCmd = new SqlCommand();
                //abre la conexion 
                SqlCmd.Connection = sqlcon;
                //nombre del SP 
                SqlCmd.CommandText = "dbo.SP_AcumulativoVentasQuiniela";
                ////tipo de ejecucion
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //se convierte el resultado en dataTable
                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(resultQuery);

            }
            catch (Exception ex)
            {
               var mensaje = ex.Message;
            }
        }
    }
}
