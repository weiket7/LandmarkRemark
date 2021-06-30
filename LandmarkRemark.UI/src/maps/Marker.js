import { openInputInfoWindow } from "./InfoWindow"
import markerRed from '../images/marker-red.png'
import markerCurrent from '../images/marker-pink-c.png'

export const addCurrentLocationMarker = (map, coordinate) => {
    new window.google.maps.Marker({
        position: coordinate,
        icon: markerCurrent,
        map: map,
    });
};

export const addMarker = (map, coordinate, note="", isLoggedIn = false) => {
    let marker = new window.google.maps.Marker({
        position: coordinate,
        map: map,
        icon: markerRed,
    });

    marker.addListener("click", function () {
        let content = "";
        if(note) {
            content = `<div class="lead">${note.remark}</div>
            <div class='text-end pt-2'>
                <input type='button' value='Close' onclick='closeInfoWindow()' class='btn btn-outline-secondary btn-sm me-1'/>
                ${isLoggedIn ? `<input type='button' value='Delete' onclick='deleteNote(${note.noteId})' class='btn btn-primary btn-sm'/>` : ""}
            </div>`;
        } else {
            content = `<input type='text' id='txt-note' class='form-control'/>
            <div class='text-end pt-2'>
                <input type='button' value='Close' onclick='closeInfoWindow()' class='btn btn-outline-secondary btn-sm me-1'/>
                <input type='button' value='Add' onclick='addNote(${coordinate.lat}, ${coordinate.lng})' class='btn btn-primary btn-sm'/>
            </div>`;
        }
        openInputInfoWindow(map, marker, content);
    });

    if(!note) {
        marker.addListener("rightclick", function () {
            removeMarker(marker);
        });
    }

    return marker;
};

export const removeMarker = (marker) => {
    if (marker.hasOwnProperty("visible")) {
        marker.setMap(null);
    }
};