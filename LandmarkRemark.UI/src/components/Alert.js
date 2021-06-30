const Alert = ({title, children, colour="primary"}) => {
    return <div className={"alert alert-" + colour}>
        {/* <span class="svg-icon svg-icon-2hx svg-icon-primary me-3">...</span> */}

        <div className="d-flex flex-column">
            <h5 className="mb-1">{ title }</h5>
            <span>{ children }</span>
        </div>
    </div>
}

export default Alert;