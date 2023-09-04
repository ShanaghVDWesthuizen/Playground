$(document).ready(function () {
    // API base URL
    const apiUrl = '/api/user';

    function getAllUsers() {
        $.ajax({
            url: apiUrl,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log('API Response:', data);

                $('#userList').empty();

                if (data && Array.isArray(data.users) && data.users.length > 0) {
                    data.users.forEach(function (user) {
                        $('#userList').append('<div>' + user.username + '</div>');
                    });
                } else {
                    $('#listOfUsersMessage').text('No Users found.');
                }
            },
            error: function (error) {
                $('#listOfUsersMessage').text('An error occurred while fetching users.');
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
