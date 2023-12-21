using Microsoft.Identity.Client;

namespace Lab200.Data;

public static class Roles
{
    public static string ADMIN = "Administrador";
    public static string MASTER = "Master";
    public static string MANAGER = "Gestor";
    public static string OPERATOR = "Operador";


    public static readonly string[] AllRoles = { ADMIN, MASTER, MANAGER, OPERATOR };
    public static readonly string[] MasterRoles = { MASTER, MANAGER, OPERATOR };
    public static readonly string[] LowerRoles = { ADMIN, MASTER, MANAGER, OPERATOR};
}
