using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lab1._2
{
    public partial class Lab1Context : DbContext
    {
        public Lab1Context()
        {
        }

        public Lab1Context(DbContextOptions<Lab1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Choice> Choices { get; set; }
        public virtual DbSet<Conseqence> Conseqences { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Fight> Fights { get; set; }
        public virtual DbSet<Hike> Hikes { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<MapX> MapXes { get; set; }
        public virtual DbSet<MapY> Mapies { get; set; }
        public virtual DbSet<PassiveUse> PassiveUses { get; set; }
        public virtual DbSet<Path> Paths { get; set; }
        public virtual DbSet<Tactic> Tactics { get; set; }
        public virtual DbSet<Tile> Tiles { get; set; }
        public virtual DbSet<Use> Uses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-U0OE9SI7; Database=Lab1; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Action>(entity =>
            {
                entity.HasKey(e => e.ActionValue)
                    .HasName("Action$PrimaryKey");

                entity.ToTable("Action");

                entity.Property(e => e.ActionValue).ValueGeneratedNever();

                entity.Property(e => e.ActionName).HasMaxLength(255);

                entity.HasOne(d => d.TacticNavigation)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.Tactic)
                    .HasConstraintName("Action$[D:\\source\\repos\\HikeMaster_be.accdb].TacticAction");
            });

            modelBuilder.Entity<Character>(entity =>
            {
                entity.HasKey(e => e.TrueId)
                    .HasName("Character$PrimaryKey");

                entity.ToTable("Character");

                entity.HasIndex(e => e.Id, "Character$CharacterId");

                entity.Property(e => e.TrueId)
                    .ValueGeneratedNever()
                    .HasColumnName("TrueID");

                entity.Property(e => e.Adjutant).HasDefaultValueSql("((0))");

                entity.Property(e => e.Agility).HasDefaultValueSql("((0))");

                entity.Property(e => e.AnimalHandling).HasDefaultValueSql("((0))");

                entity.Property(e => e.Armour).HasDefaultValueSql("((0))");

                entity.Property(e => e.Balance).HasDefaultValueSql("((0))");

                entity.Property(e => e.Bruteforce).HasDefaultValueSql("((0))");

                entity.Property(e => e.Carrying).HasDefaultValueSql("((0))");

                entity.Property(e => e.Charisma).HasDefaultValueSql("((0))");

                entity.Property(e => e.Command).HasDefaultValueSql("((0))");

                entity.Property(e => e.Constitution).HasDefaultValueSql("((0))");

                entity.Property(e => e.Cooking).HasDefaultValueSql("((0))");

                entity.Property(e => e.Craft).HasDefaultValueSql("((0))");

                entity.Property(e => e.Dexterity).HasDefaultValueSql("((0))");

                entity.Property(e => e.Discussion).HasDefaultValueSql("((0))");

                entity.Property(e => e.Dodge).HasDefaultValueSql("((0))");

                entity.Property(e => e.Epathy).HasDefaultValueSql("((0))");

                entity.Property(e => e.Foraging).HasDefaultValueSql("((0))");

                entity.Property(e => e.HandToHand).HasDefaultValueSql("((0))");

                entity.Property(e => e.Health).HasDefaultValueSql("((0))");

                entity.Property(e => e.HeavyWeapone).HasDefaultValueSql("((0))");

                entity.Property(e => e.Hunting).HasDefaultValueSql("((0))");

                entity.Property(e => e.Id).HasDefaultValueSql("((0))");

                entity.Property(e => e.Intelligence).HasDefaultValueSql("((0))");

                entity.Property(e => e.Intimidation).HasDefaultValueSql("((0))");

                entity.Property(e => e.Languages).HasDefaultValueSql("((0))");

                entity.Property(e => e.Leadership).HasDefaultValueSql("((0))");

                entity.Property(e => e.Learning).HasDefaultValueSql("((0))");

                entity.Property(e => e.Lie).HasDefaultValueSql("((0))");

                entity.Property(e => e.LieDetector).HasDefaultValueSql("((0))");

                entity.Property(e => e.LightWeapone).HasDefaultValueSql("((0))");

                entity.Property(e => e.Logistics).HasDefaultValueSql("((0))");

                entity.Property(e => e.MagickLevel).HasDefaultValueSql("((0))");

                entity.Property(e => e.MagickPower).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxEnergy).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxHealth).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxSanity).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxSatiety).HasDefaultValueSql("((0))");

                entity.Property(e => e.Medicine).HasDefaultValueSql("((0))");

                entity.Property(e => e.Memory).HasDefaultValueSql("((0))");

                entity.Property(e => e.Music).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.NowEnergy).HasDefaultValueSql("((0))");

                entity.Property(e => e.NowHealth).HasDefaultValueSql("((0))");

                entity.Property(e => e.NowSanity).HasDefaultValueSql("((0))");

                entity.Property(e => e.NowSatiety).HasDefaultValueSql("((0))");

                entity.Property(e => e.Pacification).HasDefaultValueSql("((0))");

                entity.Property(e => e.Pathfinding).HasDefaultValueSql("((0))");

                entity.Property(e => e.Perception).HasDefaultValueSql("((0))");

                entity.Property(e => e.Persuation).HasDefaultValueSql("((0))");

                entity.Property(e => e.Psycology).HasDefaultValueSql("((0))");

                entity.Property(e => e.RangedWeapone).HasDefaultValueSql("((0))");

                entity.Property(e => e.ResourceGather).HasDefaultValueSql("((0))");

                entity.Property(e => e.Science).HasDefaultValueSql("((0))");

                entity.Property(e => e.Shield).HasDefaultValueSql("((0))");

                entity.Property(e => e.Side)
                    .HasColumnName("side")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sneak).HasDefaultValueSql("((0))");

                entity.Property(e => e.Spotting).HasDefaultValueSql("((0))");

                entity.Property(e => e.Stamina).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.Strategy).HasDefaultValueSql("((0))");

                entity.Property(e => e.Strength).HasDefaultValueSql("((0))");

                entity.Property(e => e.Survival).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tactic).HasDefaultValueSql("((0))");

                entity.Property(e => e.Trade).HasDefaultValueSql("((0))");

                entity.Property(e => e.Traps).HasDefaultValueSql("((0))");

                entity.Property(e => e.Willpower).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.BattleTacticNavigation)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.BattleTactic)
                    .HasConstraintName("Character$[D:\\source\\repos\\HikeMaster_be.accdb].TacticCharacter");

                entity.HasOne(d => d.HikeNavigation)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.Hike)
                    .HasConstraintName("Character$[D:\\source\\repos\\HikeMaster_be.accdb].HikeCharacter");

                entity.HasOne(d => d.WeaponeNavigation)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.Weapone)
                    .HasConstraintName("Character$[D:\\source\\repos\\HikeMaster_be.accdb].ItemsCharacter");
            });

            modelBuilder.Entity<Choice>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.EventNavigation)
                    .WithMany(p => p.Choices)
                    .HasForeignKey(d => d.Event)
                    .HasConstraintName("Choices$[D:\\source\\repos\\HikeMaster_be.accdb].EventChoises");
            });

            modelBuilder.Entity<Conseqence>(entity =>
            {
                entity.HasKey(e => e.ConsName)
                    .HasName("Conseqence$PrimaryKey");

                entity.ToTable("Conseqence");

                entity.Property(e => e.ConsName).HasMaxLength(255);

                entity.Property(e => e.ConsValue).HasDefaultValueSql("((0))");

                entity.Property(e => e.ItemUse).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ChoiseNavigation)
                    .WithMany(p => p.Conseqences)
                    .HasForeignKey(d => d.Choise)
                    .HasConstraintName("Conseqence$[D:\\source\\repos\\HikeMaster_be.accdb].ChoisesConseqence");

                entity.HasOne(d => d.UseNameNavigation)
                    .WithMany(p => p.Conseqences)
                    .HasForeignKey(d => d.UseName)
                    .HasConstraintName("Conseqence$[D:\\source\\repos\\HikeMaster_be.accdb].UsesConseqence");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EventName).HasMaxLength(255);

                entity.Property(e => e.Safeness).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tile).HasMaxLength(255);

                entity.HasOne(d => d.TileNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Tile)
                    .HasConstraintName("Event$[D:\\source\\repos\\HikeMaster_be.accdb].TileEvent");
            });

            modelBuilder.Entity<Fight>(entity =>
            {
                entity.ToTable("Fight");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActionPoints).HasDefaultValueSql("((0))");

                entity.Property(e => e.CritStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Round).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ChoisesNavigation)
                    .WithMany(p => p.Fights)
                    .HasForeignKey(d => d.Choises)
                    .HasConstraintName("Fight$[D:\\source\\repos\\HikeMaster_be.accdb].ChoisesFight");
            });

            modelBuilder.Entity<Hike>(entity =>
            {
                entity.ToTable("Hike");

                entity.Property(e => e.HikeName).HasMaxLength(255);

                entity.HasOne(d => d.PathNavigation)
                    .WithMany(p => p.Hikes)
                    .HasForeignKey(d => d.Path)
                    .HasConstraintName("Hike$[D:\\source\\repos\\HikeMaster_be.accdb].PathHike");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ItemName).HasMaxLength(255);

                entity.Property(e => e.Mass).HasDefaultValueSql("((0))");

                entity.Property(e => e.Volume).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MapX>(entity =>
            {
                entity.HasKey(e => e.YX)
                    .HasName("Map_X$PrimaryKey");

                entity.ToTable("Map_X");

                entity.Property(e => e.YX)
                    .HasMaxLength(255)
                    .HasColumnName("Y_X");

                entity.Property(e => e.X).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.YNavigation)
                    .WithMany(p => p.MapXes)
                    .HasForeignKey(d => d.Y)
                    .HasConstraintName("Map_X$[D:\\source\\repos\\HikeMaster_be.accdb].Map_YMap_X");
            });

            modelBuilder.Entity<MapY>(entity =>
            {
                entity.HasKey(e => e.Y)
                    .HasName("Map_Y$PrimaryKey");

                entity.ToTable("Map_Y");

                entity.Property(e => e.Y).ValueGeneratedNever();
            });

            modelBuilder.Entity<PassiveUse>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Level).HasDefaultValueSql("((0))");

                entity.Property(e => e.UseName).HasMaxLength(255);

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.PassiveUses)
                    .HasForeignKey(d => d.Item)
                    .HasConstraintName("PassiveUses$[D:\\source\\repos\\HikeMaster_be.accdb].ItemsPassiveUses");
            });

            modelBuilder.Entity<Path>(entity =>
            {
                entity.ToTable("Path");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Coords).HasMaxLength(255);

                entity.HasOne(d => d.CoordsNavigation)
                    .WithMany(p => p.Paths)
                    .HasForeignKey(d => d.Coords)
                    .HasConstraintName("Path$[D:\\source\\repos\\HikeMaster_be.accdb].Map_XPath");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Path1)
                    .HasForeignKey<Path>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Path$[D:\\source\\repos\\HikeMaster_be.accdb].HikePath");
            });

            modelBuilder.Entity<Tactic>(entity =>
            {
                entity.ToTable("Tactic");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TacticName).HasMaxLength(255);

                entity.Property(e => e.TacticValue).HasMaxLength(255);

                entity.HasOne(d => d.FightNavigation)
                    .WithMany(p => p.Tactics)
                    .HasForeignKey(d => d.Fight)
                    .HasConstraintName("Tactic$[D:\\source\\repos\\HikeMaster_be.accdb].FightTactic");
            });

            modelBuilder.Entity<Tile>(entity =>
            {
                entity.HasKey(e => e.Coords)
                    .HasName("Tile$PrimaryKey");

                entity.ToTable("Tile");

                entity.Property(e => e.Coords).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Length).HasDefaultValueSql("((0))");

                entity.Property(e => e.TileName).HasMaxLength(255);

                entity.HasOne(d => d.CoordsNavigation)
                    .WithOne(p => p.Tile)
                    .HasForeignKey<Tile>(d => d.Coords)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Tile$[D:\\source\\repos\\HikeMaster_be.accdb].Map_XTile");
            });

            modelBuilder.Entity<Use>(entity =>
            {
                entity.HasIndex(e => e.UseName, "Uses$UsesUseName");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Consumable).HasMaxLength(255);

                entity.Property(e => e.UseName).HasMaxLength(255);

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.Uses)
                    .HasForeignKey(d => d.Item)
                    .HasConstraintName("Uses$[D:\\source\\repos\\HikeMaster_be.accdb].ItemsUses");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
