import React, {Component} from 'react'

class Forward extends Component
{
    shortUrl = this.props.shortUrl
    
    render()
    {
        return (
            <div className="alert alert-primary alert-dismissible fade show col-md-6 mx-auto" role="alert">
                <strong>You are being redirected. Your site will load shortly.</strong>
            </div>
        )
    }

    componentDidMount(){
        this.forward(this.shortUrl);   
    };

    forward(shortUrl)
    {
        fetch('/api'+shortUrl,
            {
                method: 'GET'
            })
            .then(response => response.json())
            .then(data => {
                window.location.replace(data.targetUrl)
            });
    }
}

export default Forward