import { addMarker } from "./Marker";

export function getMap() {
    const singaporeCoordinate = { lat: 1.3402774387496246, lng: 103.83669998815199 };
    return new window.google.maps.Map(document.getElementById("map"), {
      zoom: 12,
      center: singaporeCoordinate,
    });
}

export function mapEnableAddMarker(map) {
  map.addListener("rightclick", (mapsMouseEvent) => {
    addMarker(map, { lat: mapsMouseEvent.latLng.lat(), lng: mapsMouseEvent.latLng.lng()});
  });
}

export function appendMapScriptElement() {
    var element = document.createElement("script");
    element.type = "text/javascript";
    element.src = `https://maps.googleapis.com/maps/api/js?key=${process.env.REACT_APP_GOOGLE_MAPS_KEY}&callback=initialiseApplication`;
    document.getElementsByTagName('head')[0].appendChild(element);
}