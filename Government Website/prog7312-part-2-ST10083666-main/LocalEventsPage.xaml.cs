using PROG7312_POEPART1_ST10083666;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PROG7312_POEPART1_ST10083666
{
    public partial class LocalEventsPage : Window
    {
        // Generic data structures to organize events by date, category, and other properties
        private GenericDictionary<DateTime, Event> eventsByDate;
        private GenericDictionary<string, Event> eventsByCategory;
        private GenericQueue<Event> eventQueue;
        private GenericSet<string> uniqueCategories;
        private GenericSet<string> eventdescription;

       
        public LocalEventsPage()
        {
            InitializeComponent();
            InitializeEventData(); 
            LoadCategories();      
            ShowAllEvents();       
        }

        private void InitializeEventData()
        {
            eventsByDate = new GenericDictionary<DateTime, Event>();
            eventsByCategory = new GenericDictionary<string, Event>();
            eventQueue = new GenericQueue<Event>();
            uniqueCategories = new GenericSet<string>();
            eventdescription = new GenericSet<string>();

            //sample events
            AddEvent("Congress", "Entertainment", "new", new DateTime(2024, 11, 5));
            AddEvent("State of Nation", "Politics", "old", new DateTime(2024, 12, 12));
            AddEvent("Toytoi", "Strike", "fancy", new DateTime(2024, 12, 6));
            AddEvent("Toytoi", "Strike", "fancy", new DateTime(2024, 12, 6));
            AddEvent("Legislative Session", "Government", "Discussion and voting on new bills", new DateTime(2024, 11, 22));
            AddEvent("Mayor's Address to the Community", "Government", "Annual speech outlining municipal goals", new DateTime(2024, 12, 5));
            AddEvent("Street Lighting Upgrade Project", "Infrastructure", "Installation of new energy-efficient street lights", new DateTime(2024, 12, 1));
            AddEvent("Election Day Preparation Meeting", "Government", "Organizational meeting for the upcoming local elections", new DateTime(2024, 10, 29));
            AddEvent("City Council Meeting", "Government", "Monthly meeting to discuss city plans", new DateTime(2024, 11, 10));
            AddEvent("Community Clean-up Day", "Community", "Volunteers gather to clean local parks", new DateTime(2024, 10, 20));
            AddEvent("Public Hearing on Road Development", "Infrastructure", "Discussion on new road projects", new DateTime(2024, 11, 15));
            AddEvent("Political Rally", "Politics", "Supporters gather to back a local candidate", new DateTime(2024, 10, 30));
            AddEvent("Debate on Climate Policy", "Politics", "Panel discussion on environmental legislation", new DateTime(2024, 11, 3));
            AddEvent("Local Election Results Announcement", "Politics", "Results of the municipal elections revealed", new DateTime(2024, 11, 6));
            AddEvent("Political Party Convention", "Politics", "Annual meeting for political party strategy and nominations", new DateTime(2024, 12, 8));


        }

        private void AddEvent(string name, string category, string description, DateTime date)
        {
            // Check if an event with the same name already exists
            foreach (var key in eventsByDate.GetKeys())
            {
                var duplicateEvent = eventsByDate.Get(key).FirstOrDefault(evt => evt.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (duplicateEvent != null)
                {
                    // Remove the dupes fron event
                    eventsByDate.Get(key).Remove(duplicateEvent);
                    eventsByCategory.Get(duplicateEvent.Category).Remove(duplicateEvent);
                    eventdescription.Remove(duplicateEvent.Description);

                    // Removes dupes from category 
                    if (!eventsByCategory.Get(duplicateEvent.Category).Any())
                    {
                        uniqueCategories.Remove(duplicateEvent.Category);
                    }

                    break; 
                }
            }

            var newEvent = new Event { Name = name, Category = category, Description = description, Date = date };

            eventsByDate.Add(date, newEvent);      
            eventsByCategory.Add(category, newEvent); 
            uniqueCategories.Add(category);       
            eventdescription.Add(description);    
        }

        // Adds categories into the dropdown menu,
        private void LoadCategories()
        {
            var allCategories = new List<string> { "All" };
            allCategories.AddRange(uniqueCategories.GetItems()); // Add unique categories
            comboBoxCategory.ItemsSource = allCategories;
        }

        // Display all events in the data grid
        private void ShowAllEvents()
        {
            var allEvents = new HashSet<Event>();
            foreach (var key in eventsByDate.GetKeys())
            {
                allEvents.UnionWith(eventsByDate.Get(key)); 
            }
            dataGridEvents.ItemsSource = allEvents.ToList(); 
        }

      // Searches and filters
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string category = comboBoxCategory.SelectedItem?.ToString(); 
            DateTime? selectedDate = datePickerEvent.SelectedDate; 
            string searchText = txtSearch.Text.ToLower();
            var allEvents = new HashSet<Event>();
            var filteredEvents = new List<Event>();

          
            if (string.IsNullOrEmpty(category) && !selectedDate.HasValue && string.IsNullOrWhiteSpace(searchText))
            {
                foreach (var key in eventsByDate.GetKeys())
                {
                    allEvents.UnionWith(eventsByDate.Get(key));
                }
                filteredEvents = allEvents.ToList();
            }
            else
            {
                // Apply filters based on user input
                foreach (var key in eventsByDate.GetKeys())
                {
                    foreach (var evt in eventsByDate.Get(key))
                    {
                        bool matchesCategory = category == "All" || string.IsNullOrEmpty(category) || evt.Category.Equals(category, StringComparison.OrdinalIgnoreCase);
                        bool matchesDate = !selectedDate.HasValue || evt.Date.Date == selectedDate.Value.Date;
                        bool matchesName = string.IsNullOrWhiteSpace(searchText) || evt.Name.ToLower().Contains(searchText);

                        // Add to filtered events if all conditions match
                        if (matchesCategory && matchesDate && matchesName)
                        {
                            filteredEvents.Add(evt);
                        }
                    }
                }
            }

            dataGridEvents.ItemsSource = filteredEvents; 
            TrackUserSearch(category, selectedDate, searchText); 
            DisplayRecommendations();

            datePickerEvent.SelectedDate = null;
        }

        // Tracks what the user will be reccomended 
        private void TrackUserSearch(string category, DateTime? date, string searchText)
        {
            eventQueue = new GenericQueue<Event>(); 

            // Add events to the queue based on search text
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                foreach (var key in eventsByDate.GetKeys())
                {
                    foreach (var evt in eventsByDate.Get(key))
                    {
                        if (evt.Name.ToLower().Contains(searchText.ToLower()) && !eventQueue.Contains(evt))
                        {
                            // Add related events from the same category
                            if (eventsByCategory.ContainsKey(evt.Category))
                            {
                                foreach (var relatedEvent in eventsByCategory.Get(evt.Category))
                                {
                                    eventQueue.Enqueue(relatedEvent);
                                }
                            }
                            break; 
                        }
                    }
                }
            }
            // Add  to the queue based on selected category
            else if (!string.IsNullOrEmpty(category) && eventsByCategory.ContainsKey(category))
            {
                foreach (var evt in eventsByCategory.Get(category))
                {
                    eventQueue.Enqueue(evt);
                }
            }
        }

        // will show the 3 most recommended events
        private void DisplayRecommendations()
        {
            var recommendedEvents = eventQueue.Take(3); 
            listBoxRecommendations.ItemsSource = recommendedEvents; 
        }

      
        //Changes visibility depending on text for search
        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearchPlaceholder.Visibility = Visibility.Visible; 
            }
            else
            {
                txtSearchPlaceholder.Visibility = Visibility.Hidden; 
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }
    }
}
