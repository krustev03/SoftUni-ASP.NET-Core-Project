namespace FitnessHub.Common
{
    public static class GlobalConstants
    {
        // System & Roles Configuration
        public const string SystemName = "FitnessHub";
        public const string AdministratorRoleName = "Administrator";
        public const string TrainerRoleName = "Trainer";

        // MailKit Configuration
        public const string SupportEmail = "fitnesshubofficial2020@gmail.com";

        // SendGrid Configuration
        public const string SendGridKey = "API_KEY_HERE";

        // Cloudinary Images Configuration
        public const string CloudFolder = "FitnessHub";
        public const int ImgWidth = 600;
        public const int ImgHeight = 400;

        // Chat Page Validations
        public const int ChatMessageMinLength = 1;
        public const int ChatMessageMaxLength = 1500;

        // Contacts Page Validations
        public const int EmailMessageMinLength = 5;
        public const int EmailMessageMaxLength = 300;
        public const int ContactsNameInputMinLength = 3;
        public const int ContactsNameInputMaxLength = 45;

        // Equipments Page Validations
        public const int EquipmentNameMinLength = 5;
        public const int EquipmentNameMaxLength = 35;
        public const string EquipmentNameRegex = "^[A-Z].*?$";
        public const string EquipmentPriceRegex = @"^[+]?([0-9]+(?:[\.][0-9]*)?|\.[0-9]+)$";
        public const int EquipmentDescriptionMinLength = 20;
        public const int EquipmentDescriptionMaxLength = 250;

        // News Page Validations
        public const int NewsTitleMinLength = 5;
        public const int NewsTitleMaxLength = 40;
        public const string NewsTitleRegex = "^[A-Z].*?$";
        public const int NewsContentMinLength = 20;
        public const int NewsContentMaxLength = 900;

        // Orders Pages Validations
        public const int CardNameMinLength = 2;
        public const string CardNameRegex = "^[a-zA-Z]+(?:[\\s.]+[a-zA-Z]+)*$";
        public const string CardNumberRegex = "^[0-9]{16}$";
        public const string SecurityCodeRegex = "^[0-9]{3}$";

        public const int BuyerNameMinLength = 2;
        public const string BuyerNameRegex = "^[A-Z].*?$";
        public const int CityMinLength = 2;
        public const string CityCodeRegex = "^[0-9]{4}$";
        public const int AdressMinLength = 5;

        // Suplements Page Validation
        public const int SuplementNameMinLength = 5;
        public const int SuplementNameMaxLength = 35;
        public const string SuplementNameRegex = "^[A-Z].*?$";
        public const string SuplementPriceRegex = @"^[+]?([0-9]+(?:[\.][0-9]*)?|\.[0-9]+)$";
        public const string SuplementWeightRegex = @"^(?!0+$)\d+$";
        public const int SuplementDescriptionMinLength = 20;
        public const int SuplementDescriptionMaxLength = 250;

        // Training Schedular Pages Validations
        public const int TrainingProgramNameMinLength = 3;
        public const int TrainingProgramNameMaxLength = 30;

        public const int ExerciseNameMinLength = 2;
        public const int ExerciseNameMaxLength = 30;
        public const int ExerciseSetsMinLength = 1;
        public const int ExerciseSetsMaxLength = 30;
        public const int ExerciseRepsMinLength = 1;
        public const int ExerciseRepsMaxLength = 100;

        // Trainer Posts Page Validations
        public const int TrainerPostNameMinLength = 2;
        public const int TrainerPostNameMaxLength = 50;
        public const string TrainerPostNameRegex = "^[A-Z].*?$";

        public const int TrainerPostDescriptionMinLength = 20;
        public const int TrainerPostDescriptionMaxLength = 250;
    }
}
