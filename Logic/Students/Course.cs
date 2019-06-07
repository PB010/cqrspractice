namespace Logic.Students
{
    public class Course : Entity
    {
        public virtual string Name { get; protected set; }
        public virtual int Credits { get; protected set; }

        public Course(long id, string name, int credits)
        {
            Id = id;
            Name = name;
            Credits = credits;
        }
    }
}
