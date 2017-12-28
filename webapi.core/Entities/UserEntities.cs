using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.core.Entities
{
    /// <summary>
    /// User E
    /// </summary>
    /// <seealso cref="webapi.core.Entities.BaseEntities" />
    public class UserEntities : BaseEntities
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string userName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string password { get; set; }
        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime created { get; set; }
        /// <summary>
        /// Gets or sets the createdby.
        /// </summary>
        /// <value>
        /// The createdby.
        /// </value>
        public string createdby { get; set; }
        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>
        /// The modified.
        /// </value>
        public DateTime modified { get; set; }
        /// <summary>
        /// Gets or sets the modifiedby.
        /// </summary>
        /// <value>
        /// The modifiedby.
        /// </value>
        public string modifiedby { get; set; }
    }
}
