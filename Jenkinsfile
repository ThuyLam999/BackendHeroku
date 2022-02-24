pipeline {
    agent any 
    stages {
        stage('Push to Heroku registry') {
            withCredentials([usernamePassword(credentialsId: 'dockerhubid', passwordVariable: 'password', usernameVariable: 'username')]) {
                bat 'docker login registry.heroku.com %username% -p %password%'
                bat 'docker tag lptest999/docker_backendapi_test registry.heroku.com/test-api-9/web'
                bat 'docker push registry.heroku.com/test-api-9/web'
            }  
        }

        stage('Release the image') {
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
