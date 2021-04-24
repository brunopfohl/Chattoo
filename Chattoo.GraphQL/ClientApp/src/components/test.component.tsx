import React, { useEffect, useState } from 'react'

interface TestProps {
}

const Home: React.FC<TestProps> = (props: TestProps) => {
    const [token, setToken] = useState();

    useEffect(() => {
        const authAsync = async () => {
            //setToken(await authService.getAccessToken());
        }

        authAsync();
    });

    return (
        <div className="c-test">
            {token}
        </div>
    );
}

export default Home;