const getLocation = (successCallback) => {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(successCallback);
    } else {
        const singaporeCoordinate = { lat: 1.3554072958074483, lng: 103.83617609438667 };
        successCallback(singaporeCoordinate);
    }
}

export default getLocation;