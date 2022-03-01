pipeline {
    agent any 

    environment {
        HEROKU_API_KEY = credentials('HEROKU_API_KEY')
    }

    stages {
        stage('Push to Heroku') {
            steps {
                bat 'heroku container:release web --app=test-api-9'
            }          
        }
    
        stage('Cleanup') {
            steps {
                deleteDir()
            }
        }
    }
}
