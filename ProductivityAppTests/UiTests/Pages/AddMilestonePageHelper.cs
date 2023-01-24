using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using static System.Net.Mime.MediaTypeNames;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace ProductivityAppTests.UiTests.Pages
{
    public class AddMilestonePageHelper : BasePageHelper
    {
        readonly Query title;
        readonly Query titleTextbox;
        readonly Query descriptionTextbox;
        readonly Query deadlineTextbox;
        readonly Query cancelButton;
        readonly Query saveButton;
        readonly Query calendarYear;
        readonly Query calendarPrevMonth;
        readonly Query calendarNextMonth;
        readonly Query calendarDay;

        public AddMilestonePageHelper() 
        {
            title = c => c.Marked("Title");
            titleTextbox = c => c.ClassFull("FormsAppCompatEditText").Index(0);
            descriptionTextbox = c => c.ClassFull("FormsAppCompatEditText").Index(1);
            deadlineTextbox = c => c.Class("PickerEditText");
            cancelButton = c => c.Marked("Cancel");
            saveButton = c => c.Marked("Save");
            calendarYear = c => c.Marked("date_picker_header_year");
            calendarPrevMonth = c => c.Marked("prev");
            calendarNextMonth = c => c.Marked("next");
        }
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = title,
            iOS = title
        }; 
        
        public void EnterTitleText(string text, TimeSpan? timeout = default)
        {
            SelectElement(titleTextbox, timeout);
            app.EnterText(text);
        }

        public void EnterDescriptionText(string text, TimeSpan? timeout = default)
        {
            SelectElement(descriptionTextbox, timeout);
            app.EnterText(text);
        }

        public void SelectDeadline(DateTime selectDate, TimeSpan? timeout = default)
        {
            SelectElement(deadlineTextbox, timeout);
            app.Query(x => x.Class("DatePicker").Invoke("updateDate", selectDate.Year, selectDate.Month, selectDate.Day));
            app.Tap("OK");

        }

        public void SelectCancelButton(TimeSpan? timeout = default)
        {
            SelectElement(cancelButton, timeout);
        }

        public void SelectSaveButton(TimeSpan? timeout = default)
        {
            SelectElement(saveButton, timeout);
        }

        public AppResult GetText(string element, TimeSpan? timeout = default)
        {
            return GetElementFromQuery(element, 0, timeout);
        }

        public void SelectYear(int year)
        {
            var currentYear = DateTime.Now.Year;
            var yearStr = year.ToString();

            SelectElement(calendarYear);
            if(currentYear > year)
            {
                app.ScrollUpTo(c => c.Marked(yearStr), c => c.Class("YearPickerView"), ScrollStrategy.Gesture);
            }
            
            if(currentYear < year)
            {
                app.ScrollDownTo(c => c.Marked(yearStr), c => c.Class("YearPickerView"), ScrollStrategy.Gesture);
            }
            app.Tap(c => c.Marked(yearStr));
        }

        public void SelectMonth(int month)
        {
            var currentMonth = DateTime.Now.Month;
            var monthlyDiff = currentMonth - month;

            if (currentMonth > month)
            {
                for(int i = 0; i < monthlyDiff; i++)
                {
                    app.Tap(calendarPrevMonth);
                }
            }

            if (currentMonth < month)
            {
                monthlyDiff *= -1;
                for (int i = 0; i < monthlyDiff; i++)
                {
                    app.Tap(calendarNextMonth);
                }
            }
        }
    }
}
