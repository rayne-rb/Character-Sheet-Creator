using System.ComponentModel;

namespace CharacterSheetCreator.Shared.Enums;

public enum CustomFieldType
{
    [Description("String Field")]
    StringField,
    [Description("Integer Field")]
    IntegerField,
    [Description("Decimal Field")]
    DecimalField,
    [Description("Boolean Field")]
    BooleanField,
    [Description("Selected Field")]
    SelectedField,
}