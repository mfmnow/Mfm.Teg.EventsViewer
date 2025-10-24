import '.././App.css'
import { useState, useEffect } from 'react';
import { Dropdown, DropdownButton } from 'react-bootstrap';
import EventsList from './EventsList';

function VenuesList() {

    const [data, setData] = useState([]);
    const [venueId, setVenueId] = useState(0);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch('https://localhost:7151/api/v1.0/event-viewer/all-venues');
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const result = await response.json();
                console.log(result)
                setData(result);
            } catch (e) {
                setError(error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []); // Empty dependency array ensures this runs once on mount

    if (loading) return <p>Loading data...</p>;
    if (error) return <p>Error: {error}</p>;

    return (
        <div>
            <DropdownButton className="custom-dropdown-menu" id="dropdown-item-button" title="Select Venue">
                {data.map((venue : any, index) => (
                    <Dropdown.Item onClick={() => {
                        setVenueId(venue.id);
                    }} as="button" key={index}>{venue.label}</Dropdown.Item>
                ))}
            </DropdownButton>
            {venueId > 0 && <EventsList key={venueId} venueId={venueId}></EventsList>}
        </div>
    );
}

export default VenuesList

