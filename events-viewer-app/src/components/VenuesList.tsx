import '.././App.css'
import { useState, useEffect } from 'react';
import { Dropdown, DropdownButton } from 'react-bootstrap';
import EventsList from './EventsList';

interface Venue {
    id: number;
    label: string;
}

function VenuesList() {

    const [data, setData] = useState<Venue[]>([]);
    const [venueId, setVenueId] = useState(0);
    const [venueName, setVenueName] = useState("");
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch('https://localhost:7151/api/v1.0/event-viewer/all-venues');
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const result = await response.json();
                setData(result);
            } catch (e) {
                setError(`Cannot load Venues: ${e}`);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []); // Empty dependency array ensures this runs once on mount

    if (loading) return <p>Loading data...</p>;
    if (error != "") return <p>{error}</p>;

    return (
        <div>
            <DropdownButton className="custom-dropdown-menu" id="dropdown-item-button" title="Select Venue">
                {data.map((venue, index) => (
                    <Dropdown.Item onClick={() => {
                        setVenueId(venue.id);
                        setVenueName(venue.label);
                    }} as="button" key={index}>{venue.label}</Dropdown.Item>
                ))}
            </DropdownButton>
            {venueId > 0 && <EventsList key={venueId} name={venueName} venueId={venueId}></EventsList>}
        </div>
    );
}

export default VenuesList

