/**
* PSG.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 01/05/19 (dd/mm/yy)
* Revised on: 04/05/19 (dd/mm/yy)
*/

public class PSG : Team
{
    static int contDistBarra = 0;
    static int naoGoleiro = 0;
    static float jCdgol = 0;
    static float jEdgol = 0;
    static float jDdgol = 0;
    static int contAtkVez = 0;
    float passo = 2 * .02f;

    public override void Setup()
    {
        name = "PSG";
    }

    public override void Play()
    {
        // Dados dos jogadores
        Player jC = player1;
        Player jE = player2;
        Player jD = player3;
        jCdgol = jC.DistanceOfDefenseGolPoint();
        jEdgol = jE.DistanceOfDefenseGolPoint();
        jDdgol = jD.DistanceOfDefenseGolPoint();

        // se a bola está na frente de algum jogador, ataque!
        if (jD.DistanceOfBall() < 50 && contAtkVez < 10)
        {
            posseDeBola(jD);
            auxiliarAtk(jD, jE, jC);
            contAtkVez++;
        }
        else if (jE.DistanceOfBall() < 50 && contAtkVez > 9
              && contAtkVez < 20)
        {
            posseDeBola(jE);
            auxiliarAtk(jE, jD, jC);
            contAtkVez++;
        }
        else if (jC.DistanceOfBall() < 50 && contAtkVez > 19)
        {
            posseDeBola(jC);
            auxiliarAtk(jC, jD, jE);
            contAtkVez++;
            if (contAtkVez > 29)
            {
                contAtkVez = 0;
            }

            // se a bola não está com nenhum jogador, marquem!
        }
        else
        {

            if (Match.Instance.Ball.DistanceOfDefense(this) <= 250)
            {
                // seleciona os dois jogadores mais próximos da defesa
                if (jCdgol <= jEdgol)
                {
                    if (jEdgol <= jDdgol)
                    {
                        posicionarGoleiro(jC, jE);
                        naoGoleiro = 3;
                    }
                    else
                    {
                        posicionarGoleiro(jC, jD);
                        naoGoleiro = 2;
                    }
                }
                else
                {
                    if (jCdgol <= jDdgol)
                    {
                        posicionarGoleiro(jE, jC);
                        naoGoleiro = 3;
                    }
                    else
                    {
                        posicionarGoleiro(jE, jD);
                        naoGoleiro = 1;
                    }
                }

                // O que nao é goleiro, segue bola
                if (naoGoleiro == 1)
                {
                    jC.RegisterGoToBall();
                }
                else if (naoGoleiro == 2)
                {
                    jE.RegisterGoToBall();
                }
                else if (naoGoleiro == 3)
                {
                    jD.RegisterGoToBall();
                }
            }
            else
            {
                jC.RegisterGoToBall();
                jE.RegisterGoToBall();
                jD.RegisterGoToBall();
            }

        } // Fim defendam!

    } // Fim executar()

    bool isBolaNaEsquerda(Player jogador)
    {
        if (Match.Instance.Ball.GetPosition().y > jogador.GetPosition().y)
            return true;
        return false;
    }

    bool isBolaNaDireita(Player jogador)
    {
        if (Match.Instance.Ball.GetPosition().y < jogador.GetPosition().y)
            return true;
        return false;
    }

    void posseDeBola(Player atacante)
    {
        if (!Match.Instance.Ball.IsBallForwardOfPlayer(atacante))
        { // bola está atrás ou ao lado
            if (isBolaNaEsquerda(atacante))
            {
                if (bolaLongeEsq(atacante))
                { // bola longe na esquerda?
                    atacante.RegisterMoveToPoint(Direction.North);
                }
                else
                {
                    atacante.RegisterGoToGol();
                }
                if (bolaLongeDef(atacante))
                {
                    atacante.RegisterGoToDefense();
                }

            }
            else if (isBolaNaDireita(atacante))
            {
                if (bolaLongeDir(atacante))
                { // na direita?
                    atacante.RegisterMoveToPoint(Direction.South);
                }
                else
                {
                    atacante.RegisterGoToDefense();
                }
                if (bolaLongeDef(atacante))
                {
                    atacante.RegisterGoToDefense();
                }

            }
            else
            { // no MEIO e atrás?
                if (DistanciaLinhaLateralSuperior(atacante) < 300)
                {
                    atacante.RegisterMoveToPoint(Direction.South);
                    atacante.RegisterGoToDefense();
                }
                else
                {
                    atacante.RegisterMoveToPoint(Direction.North);
                    atacante.RegisterGoToDefense();
                }
            }

            // mas se a bola está na FRENTE do atacante, ataque!
        }
        else if (Match.Instance.Ball.IsBallForwardOfPlayer(atacante))
        {
            if (isBolaNaEsquerda(atacante))
            {
                atacante.RegisterMoveToPoint(Direction.North);
                if (!isBolaNaEsquerda(atacante))
                {
                    atacante.RegisterGoToGol();
                }
            }
            else if (isBolaNaDireita(atacante))
            { // na direita?
                atacante.RegisterMoveToPoint(Direction.South);
                if (!isBolaNaDireita(atacante))
                {
                    atacante.RegisterGoToGol();
                }
            }
            else
            { // no MEIO?
                atacante.RegisterGoToGol();
                // atacante.RegisterGoToBall(); // para testar
            }
        }
    }

    float DistanciaLinhaLateralSuperior(Player jogador)
    {
        return 5 - jogador.GetPosition().y;
    }

    float DistanciaLinhaLateralSuperior()
    {
        return 5 - Match.Instance.Ball.GetPosition().y;
    }

    // Testa se a bola está mais à Esquerda do jogador que um passo
    bool bolaLongeEsq(Player jogador)
    {
        bool bolaLonge = true;
        if (DistanciaLinhaLateralSuperior() < (passo + DistanciaLinhaLateralSuperior(jogador)))
        {
            bolaLonge = false;
        }
        return bolaLonge;
    }

    // Testa se a bola está mais à Direita do jogador que um passo
    bool bolaLongeDir(Player jogador)
    {
        bool bolaLonge = true;
        //if (bola.getDistanciaLinhaLateralDireita(this) < (21 + jogador
        //        .getDistanciaLinhaLateralDireita()))
        //{
        //    bolaLonge = false;
        //}
        return bolaLonge;
    }

    // Testa se a bola está a mais de um passo do jogador para a Defesa
    bool bolaLongeDef(Player jogador)
    {
        bool bolaLonge = true;
        //if (bola.getDistanciaLinhaFundoDefesa(this) < (21 + jogador
        //        .getDistanciaLinhaFundoDefesa()))
        //{
        //    bolaLonge = false;
        //}
        return bolaLonge;
    }

    // Testa se a bola está a mais de um passo do jogador para o Ataque
    bool bolaLongeAtk(Player jogador)
    {
        bool bolaLonge = true;
        //if (bola.getDistanciaGolAdversario(this) < (21 + jogador
        //        .getDistanciaLinhaFundoAdversario()))
        //{
        //    bolaLonge = false;
        //}
        return bolaLonge;
    }

    public void auxiliarAtk(Player atacante, Player aux1, Player aux2)
    {
        if (aux1.DistanceOfBall() > 80)
        {
            aux1.RegisterGoToBall();
        }
        else if (aux1.DistanceOfBall() < 40)
        {
            darEspaco(aux1, atacante);
        }
        if (aux2.DistanceOfBall() > 80)
        {
            aux2.RegisterGoToBall();
        }
        else if (aux2.DistanceOfBall() < 40)
        {
            darEspaco(aux2, atacante);
        }
    }

    void darEspaco(Player aux, Player atacante)
    {
        aux.RegisterGoToDefense();
    }

    void posicionarGoleiro(Player goleiro1, Player goleiro2)
    {
        // Posiciona o Goleiro1 na ponta Esq da barra para ser goleiro
        //if (goleiro1.getDistanciaGolDefesa() > 0)
        //{
        //    if (goleiro1.getDistanciaLinhaFundoDefesa() > 25)
        //    {
        //        goleiro1.RegisterGoToDefense();
        //    }
        //    if (goleiro1.getDistanciaLinhaLateralEsquerda() > 230)
        //    {
        //        goleiro1.RegisterMoveToPoint(Direction.North);
        //    }
        //    else if (goleiro1.getDistanciaLinhaLateralEsquerda() < 220)
        //    {
        //        goleiro1.RegisterMoveToPoint(Direction.South);
        //    }
        //    else
        //    { // se está na ponta esquerda
        //      // método goleiro esquerdo
        //        if (contDistBarra == 0)
        //        {
        //            goleiro1.RegisterMoveToPoint(Direction.South);
        //            contDistBarra++;
        //        }
        //        else if (contDistBarra == 1)
        //        {
        //            goleiro1.RegisterMoveToPoint(Direction.North);
        //            contDistBarra = 0;
        //        }
        //    }
        //} // Fim Posicionar Goleiro1 na ponta Esquerda

        // Posiciona o Goleiro2 na ponta Dir da barra para ser goleiro
        //if (goleiro2.getDistanciaGolDefesa() > 0)
        //{
        //    if (goleiro2.getDistanciaLinhaFundoDefesa() > 25)
        //    {
        //        goleiro2.RegisterGoToDefense();
        //    }
        //    if (goleiro2.getDistanciaLinhaLateralDireita() > 230)
        //    {
        //        goleiro2.RegisterMoveToPoint(Direction.South);
        //    }
        //    else if (goleiro2.getDistanciaLinhaLateralDireita() < 220)
        //    {
        //        goleiro2.RegisterMoveToPoint(Direction.North);
        //    }
        //    else
        //    {// se está na ponta direita
        //     // método goleiro direito
        //        if (contDistBarra == 0)
        //        {
        //            goleiro2.RegisterMoveToPoint(Direction.North);
        //            contDistBarra++;
        //        }
        //        else if (contDistBarra == 1)
        //        {
        //            goleiro1.RegisterMoveToPoint(Direction.South);
        //            contDistBarra = 0;
        //        }
        //    }
        //} // Fim Posicionar Goleiro1 na ponta Direita
    }
}
