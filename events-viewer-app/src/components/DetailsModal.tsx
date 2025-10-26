import { Button, Modal } from 'react-bootstrap';
import '.././App.css'

interface DetailsModalProps {
    show: boolean;
    handleClose: () => void;
    name: string;
    details: string;
}

function DetailsModal(props: DetailsModalProps) {    
    return (
        <Modal show={props.show} onHide={props.handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>{props.name}</Modal.Title>
            </Modal.Header>
            <Modal.Body>{props.details}</Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={props.handleClose}>Close</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default DetailsModal

