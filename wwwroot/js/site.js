$(document).ready(function () {
    // API base URL
    const apiUrl = '/api/user';

    function getAllUsers() {
        $.ajax({
            url: apiUrl,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                // Check if the response structure is as expected
                if (response.$values && Array.isArray(response.$values)) {
                    const users = response.$values;
                    if (users.length > 0) {
                        // Find the table body
                        const tbody = $('.user-table tbody');
                        if (tbody.length > 0) {
                            tbody.empty(); // Clear any existing rows

                            // Populate the table with user data
                            users.forEach(function (user) {
                                const row = $('<tr>');
                                $('<td>').text(user.userId).appendTo(row);
                                $('<td>').text(user.username).appendTo(row);
                                row.appendTo(tbody);
                            });
                        }
                    } else {
                        $('#listOfUsersMessage').text('No users found.');
                    }
                } else {
                    $('#listOfUsersMessage').text('Invalid API Response.');
                }
            },
            error: function (error) {
                $('#listOfUsersMessage').text('An error occurred while fetching users.');
                console.error('AJAX Error:', error);
            }
        });
    }

    getAllUsers();

    // Event handler for the "Add User" button
    $('#addUser').click(function () {
        const userName = $('#userName').val();

        // Create a new user object
        const newUser = {
            username: userName
        };

        // Make a POST request to add a new user
        $.ajax({
            url: apiUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newUser),
            success: function () {
                $('#addSuccessMessage').text('User has been added successfully.');
            },
            error: function () {
                $('#addSuccessMessage').text('An error occurred while adding the user.');
            }
        });
    });

    // Event handler for the "Delete User" button
    $('#deleteUser').click(function () {
        const userIdToDelete = $('#deleteUserId').val();

        // Make a DELETE request to delete the user
        $.ajax({
            url: apiUrl + '/' + userIdToDelete,
            type: 'DELETE',
            success: function () {
                $('#deleteSuccessMessage').text('User has been deleted successfully.');
            }
        });
    });

});
