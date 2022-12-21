using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSearch.Models
{
	public class CollectionEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long CollectionId { get; set; }
		public long ClickCount { get; set; }
	}
}

