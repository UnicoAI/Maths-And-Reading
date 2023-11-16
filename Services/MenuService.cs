namespace KidsMauiApp.Services;

internal static class MenuService
{
	private static readonly List<Menu> menus = new()
	{
		new()
		{
			Name = "Maths",
			Subtitle = "Exercise Now",
			HeroImage = "maths.png",
			Description = "Start Adding numbers. Improve Your Skill",
        AccentColorStart = Color.FromArgb("#a6393b"),
            AccentColorEnd = Color.FromArgb("#d17f21"),
            Images = new()
            {
                "maths.png",
                "mathicon.png",
                "maths2.png",
              
            }
		},
        new()
        {
            Name = "Reading",
            Subtitle = "Explore Universe",
            HeroImage = "reading.jpeg",
            Description = "Reading is liberating. Once we learn to read it change the overview of the world around us. Some people read to acquire information, and others read to dive deep into an imaginary world of books. ",
            AccentColorStart = Color.FromArgb("#a6393b"),
            AccentColorEnd = Color.FromArgb("#d17f21"),
            Images = new()
            {
                "reading.jpeg",
                "universe.jpeg",
                "universemenu.jpeg",
              
            }
        },

        
    };

	public static List<Menu> GetAllMenus()
		=> menus;

    public static Menu GetMenu(string menuName)
		=> menus.Where(_menu => _menu.Name == menuName).FirstOrDefault();

    public static List<Menu> GetFeaturedMenus()
    {
        var rnd = new Random();
        var randomizedMenus = menus.OrderBy(item => rnd.Next());

		return randomizedMenus.Take(2).ToList();
    }
        

}

