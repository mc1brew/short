import React, {Component} from 'react'

class Form extends Component
{
    initialState = {url: '', name: ''}
    state = this.initialState
    renderResult = this.props.renderResult

    render() {
        const {url, name} = this.state;
        return (
            <form className="form col-md-6 mx-auto">
                    <div className="mb-3">
                        <label htmlFor="url" className="form-label">Url:</label>
                        <input 
                            id="url"
                            className="form-control"
                            type="text"
                            placeholder="www.k.vin"

                            name="url"
                            value={url}
                            onChange={this.handleChange} 
                            />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="name" className="form-label">Shorter:</label>
                        <input
                            id="name"
                            className="form-control"
                            type="text"
                            placeholder="k"
                            
                            name="name"
                            value={name}
                            onChange={this.handleChange}
                            />
                    </div>
                    <div className="control">
                        <button type="button" className="btn btn-primary" onClick={() => this.addUrl()}>Add Url</button>
                    </div>
            </form>
        )
    }

    handleChange = (event) => {
        const {name, value} = event.target

        this.setState({
            [name]: value,
        })
    }

    addUrl = () => {
        this.renderResult('');
        
        fetch('/api',
        {
            method: 'POST',
            body: JSON.stringify({
                "name": this.state.name,
                "url": this.state.url
            }),
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => this.renderResult(data.redirectUrl));
    }
}

export default Form