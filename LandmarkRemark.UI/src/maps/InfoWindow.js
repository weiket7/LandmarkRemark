export function getInfoWindow() {
    window.infoWindow = new window.google.maps.InfoWindow();
}

export function openInputInfoWindow(map, marker, content) {
    window.infoWindow.setContent(content);
    window.infoWindow.open({
      anchor: marker,
      map,
      shouldFocus: true,
    });
}