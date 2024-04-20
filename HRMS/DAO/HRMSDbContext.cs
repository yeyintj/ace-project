using System;
using HRMS.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HRMS.DAO
{
	public class HRMSDbContext : DbContext
	{

		public HRMSDbContext(DbContextOptions<HRMSDbContext> dbOptions) : base(dbOptions)
		{

		}

		//register domain class(entity) as DbSets
		public DbSet<PositionEntity> Positions { get; set; }

        public DbSet<DepartmentEntity> Departments { get; set; }

        public DbSet<EmplyeeEntity> Emplyees { get; set; }

        public DbSet<DailyAttendanceEntity> DailyAttendances { get; set; }

        public DbSet<ShiftEntity> Shifts { get; set; }
    }
}

