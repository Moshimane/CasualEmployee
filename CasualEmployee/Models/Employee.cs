using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualEmployee.Api.Models
{
    public class Employee: NameEntityBase
    {
        public string Id_Number { get; set; }

        public string Last_Name { get; set; }

        public string Address { get; set; }

        public Guid Role_Id { get; set; }
        /// <summary>
        ///     Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public virtual Role Role { get; set; }

        public Guid Bank_Detail_Id { get; set; }
        /// <summary>
        ///     Gets or sets the banking details.
        /// </summary>
        /// <value>The banking details.</value>
        public virtual Bank_Detail Bank_Detail { get; set; }

        public byte[] Display_Picture { get; set; }
    }
}
