namespace WMS.Domain.Enums;

/// <summary>
/// ۱  متن تک‌خطی
/// ۲  متن چندخطی
/// ۳  عدد/کد
/// ۴  شماره تماس
/// ۵  ایمیل
/// ۶  لینک
/// ۷  تاریخ
/// ۸  فایل
/// ۹  انتخابی
/// ۱۰ لوکیشن
/// ۱۱ انتخاب مشتری
/// ۱۲ انتخاب محصول
/// ۱۳ انتخاب زمان (Time Picker)
/// </summary>
public enum CustomFieldType
{
    SingleLineText = 1,
    MultiLineText = 2,
    NumberOrCode = 3,
    Phone = 4,
    Email = 5,
    Link = 6,
    Date = 7,
    File = 8,
    Select = 9,
    Location = 10,
    CustomerSelect = 11,
    ProductSelect = 12,
    TimePicker = 13
}