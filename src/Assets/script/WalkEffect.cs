using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����Ч��
public class WalkEffect : MonoBehaviour
{
    public GameObject effect;//Ч��
    public float height;//�߶�
    public float strength;//����Ч��ǿ��
    ParticleSystem particle;//����
    private void Start()
    {
        //������Ч������ʼ����������ӳ�ʼ״̬
        particle = effect.GetComponent<ParticleSystem>();
        particle.Stop();
    }
    private void OnCollisionStay2D(Collision2D collision)//������ײ
    {
        if(collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg))//���������Ǹ���
        {
            //Debug.Log(collision.gameObject.tag);
            if(rg.velocity.magnitude != 0)//�ٶȲ�Ϊ��
            {
                effect.active = true;//��ʼЧ��
                effect.transform.position = new Vector3(rg.transform.position.x, this.transform.position.y + height, 0);//���ӷ����ĵ�λ�ã�rg���£������ĸ߶�+ԭ���ĸ߶ȣ�
                particle.emissionRate = strength* (rg.velocity.magnitude/10f);//ǿ��*�ٶȵ�0.1��
            }
            else
            {
                particle.Stop();//�ٶ�Ϊ��ֹͣ����
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg))
        {
            particle.Stop();//�˳�ʱֹͣ����
        }
    }
    //Ϊʲô���������Ҫ�ӱ���trigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg))
        {
            //Debug.Log(collision.gameObject.tag);
            if (rg.velocity.magnitude != 0)
            {
                effect.active = true;
                effect.transform.position = new Vector3(rg.transform.position.x, this.transform.position.y + height, 0);
                particle.emissionRate = strength * (rg.velocity.magnitude / 10f);
            }
            else
            {
                particle.Stop();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rg))
        {
            particle.Stop();
        }
    }
}
