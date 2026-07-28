using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.App.Constants
{
    // List of system tables that are used for code generation, and should be excluded from user defined tables in the system.
    // they can be refrenced by auth systems that manage the access to these system tables.
    public static class SystemTables
    {
        public static string[] WhiteList  =
        [
            "BfsTenant",
            "BfsTenantSystem",

            "BfsSystem",
            "BfsComponent",
            "BfsField",

            "BfsComponentSystemAction",
            "BfsComponentBusinessAction",
            "SystemAction",
            "BusinessAction",
        ];
    }
}
