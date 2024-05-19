// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const url = 'https://tripadvisor16.p.rapidapi.com/api/v1/hotels/searchHotelsByLocation?latitude=40.730610&longitude=-73.935242&checkIn=2024-05-21&checkOut=2024-06-04&pageNumber=1&currencyCode=USD';
const options = {
    method: 'GET',
    headers: {
        'X-RapidAPI-Key': '7726e13a05msh99dd95f9cce01b7p126ecdjsn413621b55be2',
        'X-RapidAPI-Host': 'tripadvisor16.p.rapidapi.com'
    }
};

async function fetchHotels() {
    try {
        const response = await fetch(url, options);
        const result = await response.json();
        displayHotels(result.data.data);
    } catch (error) {
        console.error(error);
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
        hotelImage.src = hotel.cardPhotos[0].sizes.urlTemplate.replace('{width}', '450').replace('{height}', '300');
        hotelImage.alt = hotel.title;

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