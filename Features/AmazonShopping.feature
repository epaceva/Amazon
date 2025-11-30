Feature: AmazonShopping
	As a user
	I want to search for a book on Amazon
	So that I can verify its details, badge status, and look inside basket functionality

@chrome
Scenario: Search for Harry Potter book and verify extended details
	Given I navigate to "https://www.amazon.co.uk"
	And I accept cookies if prompted
	When I search for "Harry Potter and the Cursed Child" in the "Books" section
	
	# Verify Results Page
	Then the first item should have the title "Harry Potter and the Cursed Child - Parts One and Two"
	And I verify if the first item has a badge
	And the selected type should be "Paperback"
	And I store the price of the first item
	
	# Go to Details Page
	When I navigate to the book details page
	Then the book title on details page should match the search result
	And I verify if the book has a badge on the details page
	And the price should match the stored price from search result
	
	# Add to Basket & Verify Notification
	When I add the book to the basket
	Then I should see a notification with title "Added to Basket"
	And the basket subtotal should show 1 item
	
	# Go to Basket & Verify Final Details
	When I click on edit the basket
	Then the book is shown on the list
	And Verify that the title, type of print, price, quantity is 1, and total price are correct