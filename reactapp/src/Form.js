import React, {Component} from 'react'

class Form extends Component
{
    initialState = {url: '', key: ''}
    state = this.initialState
    updateRedirectUrl = this.props.updateRedirectUrl

    render() {
        const {url, key} = this.state;
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
                            id="key"
                            className="form-control"
                            type="text"
                            placeholder="k"
                            
                            name="key"
                            value={key}
                            onChange={this.handleChange}
                            />
                    </div>
                    <div className="control">
                        <button type="button" className="btn btn-primary" onClick={() => this.createLink()}>Create Link</button>
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

    createLink = () => {
        this.updateRedirectUrl('');
        
        fetch('/api',
        {
            method: 'POST',
            body: JSON.stringify({
                "key": this.state.key,
                "targetUrl": this.state.url
            }),
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            this.updateRedirectUrl(data.shortenedUrl)
        });
    }
}

export default Form