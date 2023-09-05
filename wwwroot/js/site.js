$(document).ready(function () {
    // API base URL
    const apiUrl = '/api/user';

    function getAllUsers() {
        $.ajax({
            url: apiUrl,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.$values && Array.isArray(response.$values)) {
                    const users = response.$values;
                    if (users.length > 0) {

                        const tbody = $('.user-table tbody');
                        if (tbody.length > 0) {
                            tbody.empty();

                            users.forEach(function (user) {
                                const row = $('<tr>');
                                $('<td>').text(user.userId).appendTo(row);
                                $('<td>').text(user.username).appendTo(row);

                                const deleteButton = $('<button>').text('Delete').click(function () {
                                    deleteUser(user.userId);
                                });

                                $('<td>').append(deleteButton).appendTo(row);

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

    function deleteUser(userId) {
        if (confirm('Are you sure you want to delete this user?')) {
            // Send an HTTP DELETE request to delete the user
            $.ajax({
                url: apiUrl + '/' + userId,
                type: 'DELETE',
                success: function () {
                    $('#userRow_' + userId).remove();
                    getAllUsers();
                },
                error: function (error) {
                    console.error('Error deleting user:', error);
                }
            });
        }
    }
});
