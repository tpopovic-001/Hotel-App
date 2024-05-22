// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const url = (() => {
    const baseUrl = 'https://tripadvisor16.p.rapidapi.com/api/v1/hotels/searchHotelsByLocation';
    const latitude = 40.730610;
    const longitude = -73.935242;
    const checkIn = new Date();
    checkIn.setDate(checkIn.getDate() + 1); // za Check-in
    const checkOut = new Date();
    checkOut.setDate(checkOut.getDate() + 8); // za Check-out

    const formattedCheckIn = checkIn.toISOString().split('T')[0];
    const formattedCheckOut = checkOut.toISOString().split('T')[0];

    return `${baseUrl}?latitude=${latitude}&longitude=${longitude}&checkIn=${formattedCheckIn}&checkOut=${formattedCheckOut}&pageNumber=1&currencyCode=USD`;
})();

const options = {
    method: 'GET',
    headers: {
        'X-RapidAPI-Key': 'aab9d3409emsh4dec1d7b43abf77p187cf8jsn95dc745d2b8c',
        'X-RapidAPI-Host': 'tripadvisor16.p.rapidapi.com'
    }
};

async function fetchHotels() {
    try {
        const response = await fetch(url, options);
        const result = await response.json();
        console.log(result);
        if (result.status && result.data && result.data.data) {
            displayHotels(result.data.data);
        } else {
            console.error('Unexpected API response structure:', result);
        }
    } catch (error) {
        console.error('Error fetching hotels:', error);
    }
}

function displayHotels(hotels) {
    const hotelsContainer = document.getElementById('APIHotels').querySelector('.row');

    if (!hotelsContainer) {
        console.error('Element with id "APIHotels" not found');
        return;
    }

    hotels.forEach(hotel => {
        const colDiv = document.createElement('div');
        colDiv.classList.add('col-md-6');

        const cardDiv = document.createElement('div');
        cardDiv.classList.add('card', 'mb-4');

        const hotelImage = document.createElement('img');
        hotelImage.classList.add('card-img-top');

        const imageUrl = hotel.cardPhotos[0]?.sizes?.urlTemplate;

        if (imageUrl) {
            const replacedUrl = imageUrl.replace('{width}', '450').replace('{height}', '300');
            hotelImage.src = replacedUrl;
        } else {
            hotelImage.src = 'placeholder.jpg';
            hotelImage.alt = 'No image available';
        }

        const cardBodyDiv = document.createElement('div');
        cardBodyDiv.classList.add('card-body');

        const cardTitle = document.createElement('h5');
        cardTitle.classList.add('card-title');
        cardTitle.textContent = hotel.title;

        const cardTextLocation = document.createElement('p');
        cardTextLocation.classList.add('card-text');
        cardTextLocation.textContent = hotel.secondaryInfo;

        const cardTextRating = document.createElement('p');
        cardTextRating.classList.add('card-text');
        cardTextRating.textContent = `Rating: ${hotel.bubbleRating.rating} (${hotel.bubbleRating.count} reviews)`;

        cardBodyDiv.appendChild(cardTitle);
        cardBodyDiv.appendChild(cardTextLocation);
        cardBodyDiv.appendChild(cardTextRating);

        cardDiv.appendChild(hotelImage);
        cardDiv.appendChild(cardBodyDiv);
        colDiv.appendChild(cardDiv);
        hotelsContainer.appendChild(colDiv);
    });
}

document.addEventListener('DOMContentLoaded', fetchHotels);


