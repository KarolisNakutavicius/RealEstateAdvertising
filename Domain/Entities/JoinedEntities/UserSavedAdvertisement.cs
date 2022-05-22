namespace Domain.Entities.JoinedEntities;

public class UserSavedAdvertisement
{
    public int Id { get; set; }
    
    public User User { get; set; }
    
    public Advertisement Advertisement { get; set; }
}